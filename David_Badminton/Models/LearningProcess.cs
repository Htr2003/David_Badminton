using System;
using System.Collections.Generic;

namespace David_Badminton.Models;

public partial class LearningProcess
{
    public int LearningProcessId { get; set; }

    public int StudentId { get; set; }

    public string Title { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public int IsPublish { get; set; }

    public string LinkWebsite { get; set; } = null!;

    public string ImagesThumb { get; set; } = null!;

    public string ImagesPath { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }
}
