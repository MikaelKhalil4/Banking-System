using System;
using System.Collections.Generic;

namespace TransactionService.Domain.Entities;

public partial class RecurrentTransaction
{
    public long Id { get; set; }

    public long TransactionId { get; set; }

    public string BgJobId { get; set; }

    public virtual Transaction Transaction { get; set; }= null!;
}
