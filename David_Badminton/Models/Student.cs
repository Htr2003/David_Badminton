using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int FacilityId { get; set; }

    public int CoachId { get; set; }

    public int TimeId { get; set; }

    public int TypeUserId { get; set; }
    //---------------------------------
    public string StudentCode { get; set; } = null!;
    //---------------------------------

    public string StudentName { get; set; } = null!;

    public string Images { get; set; } = null!;

    public int GenderId { get; set; }

    public DateTime Birthday { get; set; }

    public string Phone { get; set; } = null!;

    public int StatusId { get; set; }

    public string Address { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Level { get; set; }

    public string Email { get; set; } = null!;

    // Decimal type Tuitions
    public decimal Tuitions { get; set; }
    //----------------------

    public DateTime StudyStart { get; set; }

    public string HealthCondition { get; set; } = null!;

    public string Height { get; set; } = null!;

    public string Weight { get; set; } = null!;

    public string GuardianName { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public string GuardianPhone { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string UserCreated { get; set; } = null!;

    public string UserUpdated { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual ICollection<RollCall> RollCalls { get; set; } = new List<RollCall>();

    public virtual Time Time { get; set; } = null!;

    public virtual TypeUser TypeUser { get; set; } = null!;
}
