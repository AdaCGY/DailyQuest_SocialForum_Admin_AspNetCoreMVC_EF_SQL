using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int TaskTypeId { get; set; }

    public int TaskLabelId { get; set; }

    public string TaskContent { get; set; } = null!;

    public int? SubTaskId { get; set; }

    public DateTime ExpectDate { get; set; }

    public string? SetPeriod { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public int Points { get; set; }

    public int ToolId { get; set; }

    public int TaskResultId { get; set; }

    public virtual SubTask? SubTask { get; set; }

    public virtual ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();

    public virtual TaskLabel TaskLabel { get; set; } = null!;

    public virtual TaskResult TaskResult { get; set; } = null!;

    public virtual TaskType TaskType { get; set; } = null!;

    public virtual Tool Tool { get; set; } = null!;

    public virtual ICollection<UserAndTaskR> UserAndTaskRs { get; set; } = new List<UserAndTaskR>();
}
