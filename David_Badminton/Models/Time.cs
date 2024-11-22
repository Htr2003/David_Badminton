using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class Time
{
    public int TimeId { get; set; }

    public string TimeName { get; set; } = null!;

    public string UserCreated { get; set; } = null!;

    public string UserUpdated { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
