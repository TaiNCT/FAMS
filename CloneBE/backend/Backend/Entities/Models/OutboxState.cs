using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class OutboxState
{
    public Guid OutboxId { get; set; }

    public Guid LockId { get; set; }

    public byte[]? RowVersion { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Delivered { get; set; }

    public long? LastSequenceNumber { get; set; }
}
