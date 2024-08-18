using System;
using System.Collections.Generic;

namespace UserAccountService.Domain.Entities;

public partial class Account
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public long BranchId { get; set; }

    public decimal Balance { get; set; }

    public DateTime  CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual User User { get; set; }= null!;
}
