using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class ConversationHistory
{
    public int MessageId { get; set; }

    public int? SenderMemberId { get; set; }

    public int? ReceiverMemberId { get; set; }

    public string? MessageContent { get; set; }

    public DateTime? SendTime { get; set; }

    public int? StatusId { get; set; }

    public int? ReplyMessageId { get; set; }

    public virtual ICollection<ConversationHistory> InverseReplyMessage { get; set; } = new List<ConversationHistory>();

    public virtual Member? ReceiverMember { get; set; }

    public virtual ConversationHistory? ReplyMessage { get; set; }

    public virtual Member? SenderMember { get; set; }

    public virtual MessageStatus? Status { get; set; }
}
