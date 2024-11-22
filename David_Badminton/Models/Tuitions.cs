namespace David_Badminton.Models
{
    public partial class Tuitions
    {
        public int TuitionId { get; set; }

        public int StudentId { get; set; }

        public int IsCheck { get; set; }

        public int IsNull { get; set; }

        public int Money { get; set; }

        public string UserCreated { get; set; } = null!;

        public string UserUpdated { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
