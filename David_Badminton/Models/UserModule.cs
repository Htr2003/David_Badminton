using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class UserModule
{
    public decimal UserModuleId { get; set; }

    public decimal ModuleId { get; set; }

    public int CoachId { get; set; }

    public int IsView { get; set; }

    public int IsInsert { get; set; }

    public int IsDelete { get; set; }

    public int IsUpdate { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual ModuleAccess Module { get; set; } = null!;
}
