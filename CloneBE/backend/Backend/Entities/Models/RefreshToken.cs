using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class RefreshToken
{
    public Guid Id { get; set; }

    public string RefreshTokenId { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string UserId { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
