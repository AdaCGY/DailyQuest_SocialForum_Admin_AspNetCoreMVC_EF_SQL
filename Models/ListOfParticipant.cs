using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class ListOfParticipant
{
    public int TaskId { get; set; }

    public int ParticipantId { get; set; }

    public bool IsOrganizer { get; set; }

    public virtual Member Participant { get; set; } = null!;
}
