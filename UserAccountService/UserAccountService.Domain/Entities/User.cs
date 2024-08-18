using System;
using System.Collections.Generic;

namespace UserAccountService.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }

    public int BranchId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Branch Branch { get; set; }= null!;

    public virtual Role? Role { get; set; }
}
