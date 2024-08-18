namespace TransactionService.API.StartUp;

public class RabitMqStartup: BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RabitMqListners _rabitMqListners;
    public RabitMqStartup(IServiceScopeFactory scopeFactory, RabitMqListners rabitMqListners)
    {
        _scopeFactory = scopeFactory;
        _rabitMqListners = rabitMqListners;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _rabitMqListners.StartListening();
    }
}
