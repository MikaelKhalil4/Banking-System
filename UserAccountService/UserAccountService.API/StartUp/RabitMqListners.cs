using Foxera.RabitMq;
using Microsoft.EntityFrameworkCore;
using UserAccountService.Contracts.Persistence;
using UserAccountService.Domain.Entities;

namespace UserAccountService.API.StartUp;

public class RabitMqListners
{
    private readonly RabbitMQService _rabbitMqService;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabitMqListners(RabbitMQService rabbitMqService, IServiceScopeFactory scopeFactory)
    {
        _rabbitMqService = rabbitMqService;
        _scopeFactory = scopeFactory;
    }
    
    public async Task StartListening()
    {
        _rabbitMqService.Receive<Account>("account_balance_update_task", HandleMessage);
    }

    private async Task  HandleMessage(Account updatedAccount)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IAccountsDbContext>();

            var acc = await dbContext.Accounts.SingleOrDefaultAsync(s => s.Id == updatedAccount.Id);
            acc.Balance = updatedAccount.Balance;//updating the balance
            
            await dbContext.SaveChangesAsync();
        }
    }
    
}