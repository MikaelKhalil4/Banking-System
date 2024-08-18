namespace TransactionService.Application.Constants
{
    public class ApplicationConstants
    {
        public static class CachingKeys
        {
            public const string User = nameof(User);
        }
        
        public static class TransactionTypeConstants
        {
            public const string Deposit = "Deposit";
            public const string Withdrawal = "Withdrawal";
        }
    }
}
