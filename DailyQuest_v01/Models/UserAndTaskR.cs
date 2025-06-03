using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class UserAndTaskR
{
    public int MemberId { get; set; }

    public int TaskId { get; set; }

    public DateTime EndDate { get; set; }

    public int TaskResultId { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Mission Task { get; set; } = null!;

    public virtual TaskResult TaskResult { get; set; } = null!;
}
