using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;


namespace David_Badminton.Models;

public partial class Coach 
{
    public int CoachId { get; set; }

    public string CoachName { get; set; } = null!;

    public string? Images { get; set; }

    public int FacilityId { get; set; }

    public int TypeUserId { get; set; }

    public int GenderId { get; set; }

    public string Email { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int StatusId { get; set; }

    public int Education { get; set; }

    public string Experience { get; set; } = null!;

    public int Level { get; set; }

    public string TaxCode { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public string BankNumber { get; set; } = null!;

    public DateTime WorkingStart { get; set; }

    public string HealthCondition { get; set; } = null!;

    public string Cccd { get; set; } = null!;

    public string PlaceOfOrigin { get; set; } = null!;

    public string PlaceOfResidence { get; set; } = null!;

    public decimal? Salary { get; set; }

    public int MaritalStatus { get; set; }

    public string Description { get; set; } = null!;

    public string NamePerson { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string UserCreated { get; set; } = null!;

    public string UserUpdated { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual Facility Facility { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RollCall> RollCalls { get; set; } = new List<RollCall>();

    public virtual TypeUser TypeUser { get; set; } = null!;

    public virtual ICollection<UserModule> UserModules { get; set; } = new List<UserModule>();
}
