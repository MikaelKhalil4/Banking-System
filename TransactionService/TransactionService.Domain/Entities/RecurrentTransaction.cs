using System;
using System.Collections.Generic;

namespace TransactionService.Domain.Entities;

public partial class RecurrentTransaction
{
    public int Id { get; set; }

    public int TransactionId { get; set; }

    public int BgJobId { get; set; }

    public virtual Transaction Transaction { get; set; }= null!;
}
