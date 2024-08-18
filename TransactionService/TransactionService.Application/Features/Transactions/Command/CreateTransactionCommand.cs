using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Foxera.Common.CustomExceptions;
using Foxera.RabitMq;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Constants;
using TransactionService.Contracts.Persistence;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Features.Transactions.Command;

public class CreateTransactionCommand : IRequest<TransactionViewModel>
{
    public int AccountId { get; set; } //this account shall ber restricted to the user that's authenticating

    public string Amount { get; set; }

    public bool IsRecurrent { get; set; }
}

public class TransactionViewModelRequest
{
    [Required] public int AccountId { get; set; } //this account shall ber restricted to the user that's authenticating

    [Required]
    [RegularExpression(@"^[+-]\d+$", ErrorMessage = "Amount must start with '+' or '-' followed by digits.")]
    public string Amount { get; set; }
}

public class TransactionViewModel
{
    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public int TransactionTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CurrencyId { get; set; }

    public bool IsDeleted { get; set; }
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionViewModel>
{
    private readonly ITransactionsDbContext _context;
    private readonly IMapper _mapper;
    public readonly IRabbitMQService _RabbitMqService;

    public CreateTransactionCommandHandler(ITransactionsDbContext context, IMapper mapper, IRabbitMQService rabbitMqService)
    {
        _context = context;
        _mapper = mapper;
        _RabbitMqService = rabbitMqService;
    }

    public async Task<TransactionViewModel> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        // Determine the transaction type based on the amount's sign
        var transactionTypeName = request.Amount.StartsWith("+")
            ? ApplicationConstants.TransactionTypeConstants.Deposit
            : ApplicationConstants.TransactionTypeConstants.Withdrawal;

        // Fetch the corresponding TransactionType from the database
        var transactionType = await _context.Transactiontypes
            .FirstOrDefaultAsync(t => t.TypeName == transactionTypeName, cancellationToken);


        // Parse the amount to a decimal (ignoring the '+' or '-' sign)
        var amount = ParseAmount(request.Amount);

        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == request.AccountId);


        if (transactionTypeName == ApplicationConstants.TransactionTypeConstants.Deposit)
        {
            account.Balance += amount;
        }
        else if (transactionTypeName == ApplicationConstants.TransactionTypeConstants.Withdrawal)
        {
            // Ensure there is enough balance to perform the withdrawal
            if (account.Balance >= amount)
            {
                account.Balance -= amount;
            }
            else
            {
                throw new BadRequestException("Insufficient balance.");
            }
        }

        _RabbitMqService.Send("account_balance_update_task", account);//it containts el account id , balance

        
        var transactionViewModel = new TransactionViewModel
        {
            AccountId = request.AccountId,
            Amount = amount, // Store as a positive or negative value based on the input
            TransactionTypeId = transactionType.Id,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            CurrencyId = 1, // Assuming a default currency for simplicity
            IsDeleted = false
        };

        Transaction newTransaction = _mapper.Map<Transaction>(transactionViewModel);


        if (request.IsRecurrent)
        {
            // Save the initial transaction and get its ID
            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync(cancellationToken);     

            // Generate the job ID
            string jobId = $"RecurrentTransaction/{newTransaction.Id}";

            RecurringJob.AddOrUpdate<CreateTransactionCommandHandler>(jobId,
                x => x.AddRecurrentTransaction(newTransaction.Id),
                "0 0 1 * *"); // Runs at midnight on the 1st day of each month

            var recurentTransaction = new RecurrentTransaction
            {
                BgJobId = jobId
            };

            _context.RecurrentTransactions.Add(recurentTransaction);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync(cancellationToken);
        }


        return transactionViewModel;
    }


    public async Task AddRecurrentTransaction(long transactionId)
    {
        // Retrieve the original transaction to base the new one on
        var originalTransaction = await _context.Transactions.FindAsync(transactionId);
    
        if (originalTransaction != null)
        {
            // Create a new transaction based on the original one
            var newTransaction = new Transaction
            {
                AccountId = originalTransaction.AccountId,
                Amount = originalTransaction.Amount,
                TransactionTypeId = originalTransaction.TransactionTypeId,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                CurrencyId = originalTransaction.CurrencyId,
                IsDeleted = false
            };

            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync();
        }
    }

    private decimal ParseAmount(string input)
    {
        // Remove the leading '+' or '-' and parse the remaining number
        return decimal.Parse(input.Substring(1), System.Globalization.CultureInfo.InvariantCulture) *
               (input.StartsWith("-") ? -1 : 1);
    }
}