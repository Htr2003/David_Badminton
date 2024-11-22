using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class RollCallCoach
{
    public int RollCallCoachId { get; set; }

    public int CoachId { get; set; }

    public int StatusId { get; set; }

    public int IsCheck { get; set; }

    public string UserCreated { get; set; } = null!;

    public string UserUpdated { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }
}
