using System;
using System.Collections.Generic;

namespace exam.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public virtual ICollection<Examresult> Examresults { get; } = new List<Examresult>();
}
