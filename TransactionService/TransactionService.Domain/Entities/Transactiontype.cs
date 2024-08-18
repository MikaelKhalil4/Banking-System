using System;
using System.Collections.Generic;

namespace TransactionService.Domain.Entities;

public partial class Transactiontype
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
