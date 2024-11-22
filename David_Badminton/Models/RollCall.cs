using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class RollCall
{
    public int RollCallId { get; set; }

    public int StudentId { get; set; }

    public int CoachId { get; set; }

    public int IsCheck { get; set; }

    //---------------------------------
    public int IsNull { get; set; }
    //---------------------------------


    public int StatusId { get; set; }

    public string UserCreated { get; set; } = null!;

    public string UserUpdated { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
