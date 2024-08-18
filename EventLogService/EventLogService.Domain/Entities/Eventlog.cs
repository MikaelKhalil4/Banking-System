using System;
using System.Collections.Generic;

namespace EventLogService.Domain.Entities;

public partial class Eventlog
{
    public int Id { get; set; }

    public int? EventTypeId { get; set; }

    public string EventData { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? TransactionId { get; set; }

    public virtual Eventtype? EventType { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
