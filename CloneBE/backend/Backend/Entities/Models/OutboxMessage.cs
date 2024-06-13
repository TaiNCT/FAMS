using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class OutboxMessage
{
    public long SequenceNumber { get; set; }

    public DateTime? EnqueueTime { get; set; }

    public DateTime SentTime { get; set; }

    public string? Headers { get; set; }

    public string? Properties { get; set; }

    public Guid? InboxMessageId { get; set; }

    public Guid? InboxConsumerId { get; set; }

    public Guid? OutboxId { get; set; }

    public Guid MessageId { get; set; }

    public string ContentType { get; set; } = null!;

    public string MessageType { get; set; } = null!;

    public string Body { get; set; } = null!;

    public Guid? ConversationId { get; set; }

    public Guid? CorrelationId { get; set; }

    public Guid? InitiatorId { get; set; }

    public Guid? RequestId { get; set; }

    public string? SourceAddress { get; set; }

    public string? DestinationAddress { get; set; }

    public string? ResponseAddress { get; set; }

    public string? FaultAddress { get; set; }

    public DateTime? ExpirationTime { get; set; }
}
