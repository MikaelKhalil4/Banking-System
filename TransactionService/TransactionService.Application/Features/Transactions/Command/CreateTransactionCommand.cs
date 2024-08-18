using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Constants;
using TransactionService.Contracts.Persistence;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Features.Transactions.Command;


public class CreateTransactionCommand : IRequest<TransactionViewModel>
{
    [Required]
    public int AccountId { get; set; }//this account shall ber restricted to the user that's authenticating
    
    [Required]
    [RegularExpression(@"^[+-]\d+$", ErrorMessage = "Amount must start with '+' or '-' followed by digits.")]
    public string Amount  { get; set; }
    
    
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


    public CreateTransactionCommandHandler(ITransactionsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TransactionViewModel> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
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
 
        _context.Transactions.Add(newTransaction);
        await _context.SaveChangesAsync(cancellationToken);

        return transactionViewModel;
    }

    private bool IsValidInput(string input)
    {
        return input.StartsWith("+") || input.StartsWith("-");
    }

    private decimal ParseAmount(string input)
    {
        // Remove the leading '+' or '-' and parse the remaining number
        return decimal.Parse(input.Substring(1), System.Globalization.CultureInfo.InvariantCulture) * 
               (input.StartsWith("-") ? -1 : 1);
    }

}