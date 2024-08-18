using System;
using System.Collections.Generic;

namespace EventLogService.Domain.Entities;

public partial class Transaction
{
    public int Id { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Eventlog> Eventlogs { get; set; } = new List<Eventlog>();
}
