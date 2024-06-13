using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class InboxState
{
    public long Id { get; set; }

    public Guid MessageId { get; set; }

    public Guid ConsumerId { get; set; }

    public Guid LockId { get; set; }

    public byte[]? RowVersion { get; set; }

    public DateTime Received { get; set; }

    public int ReceiveCount { get; set; }

    public DateTime? ExpirationTime { get; set; }

    public DateTime? Consumed { get; set; }

    public DateTime? Delivered { get; set; }

    public long? LastSequenceNumber { get; set; }
}
