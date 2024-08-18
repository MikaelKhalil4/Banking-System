using System;
using System.Collections.Generic;

namespace TransactionService.Domain.Entities;

public partial class Transaction
{
    public long Id { get; set; }

    public long AccountId { get; set; }

    public decimal Amount { get; set; }

    public int TransactionTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CurrencyId { get; set; }

    public bool IsDeleted { get; set; }
    
    

    public virtual Account Account { get; set; }= null!;

    public virtual Currency Currency { get; set; }= null!;

    public virtual ICollection<RecurrentTransaction> RecurrentTransactions { get; set; } = new List<RecurrentTransaction>();

    public virtual Transactiontype? TransactionType { get; set; }
}
