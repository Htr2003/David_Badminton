using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public string FacilityName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Longtitude { get; set; }

    public decimal Latitude { get; set; }

    public string UserCreated { get; set; } = null!;

    public string UserUpdated { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();
}
