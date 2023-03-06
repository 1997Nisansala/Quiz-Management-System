using System;
using System.Collections.Generic;

namespace exam.Models;

public partial class Teacher
{
    public string TeacherId { get; set; } = null!;

    public string TeacherName { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; } = new List<Exam>();
}
