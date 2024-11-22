using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class TypeUser
{
    public int TypeUserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
