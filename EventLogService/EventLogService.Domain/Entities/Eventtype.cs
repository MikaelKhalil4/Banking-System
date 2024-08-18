using System;
using System.Collections.Generic;

namespace EventLogService.Domain.Entities;

public partial class Eventtype
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Eventlog> Eventlogs { get; set; } = new List<Eventlog>();
}
