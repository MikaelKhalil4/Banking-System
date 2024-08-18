using Foxera.RabitMq;
using TransactionService.Contracts.Persistence;
using TransactionService.Domain.Entities;
using TransactionService.Persistence.Context;

namespace TransactionService.API.StartUp;

public class RabitMqListners
{
    private readonly IServiceScopeFactory _scopeFactory;

    public RabitMqListners(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task StartListening()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var _rabbitMqService = scope.ServiceProvider.GetRequiredService<RabbitMQService>();
            _rabbitMqService.Receive<Account>("account_creation_task", HandleMessage);
        }
    }

    private async Task  HandleMessage(Account newAccount)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ITransactionsDbContext>();
            await dbContext.Accounts.AddAsync(newAccount);
            await dbContext.SaveChangesAsync();
        }
    }
    
}