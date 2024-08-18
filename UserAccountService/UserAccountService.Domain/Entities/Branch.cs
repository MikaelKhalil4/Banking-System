using System;
using System.Collections.Generic;

namespace UserAccountService.Domain.Entities;

public partial class Branch
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int LocationId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Location Location { get; set; }= null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
