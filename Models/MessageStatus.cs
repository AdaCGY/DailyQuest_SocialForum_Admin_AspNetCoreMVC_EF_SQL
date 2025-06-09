using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MessageStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<ConversationHistory> ConversationHistories { get; set; } = new List<ConversationHistory>();
}
