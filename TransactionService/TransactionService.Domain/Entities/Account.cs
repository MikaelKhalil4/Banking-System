using System;
using System.Collections.Generic;

namespace TransactionService.Domain.Entities;

public partial class Account
{
    public int Id { get; set; }

    public decimal Balance { get; set; }

    public int BranchId { get; set; }

    public bool IsDeleted { get; set; }
    
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
