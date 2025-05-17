using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class UserAndTaskR
{
    public int UserId { get; set; }

    public int TaskId { get; set; }

    public DateTime EndDate { get; set; }

    public int TaskResultId { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual TaskResult TaskResult { get; set; } = null!;

    public virtual Member User { get; set; } = null!;
}
