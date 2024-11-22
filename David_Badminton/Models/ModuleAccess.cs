using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class ModuleAccess
{
    public decimal ModuleId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string UserCreated { get; set; } = null!;

    public string UserUpdated { get; set; } = null!;

    public virtual ICollection<UserModule> UserModules { get; set; } = new List<UserModule>();
}
