using System;
using System.Collections.Generic;

namespace UserAccountService.Domain.Entities;

public partial class Location
{
    public int Id { get; set; }

    public string LocationName { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
