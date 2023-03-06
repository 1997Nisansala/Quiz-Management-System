using System;
using System.Collections.Generic;

namespace exam.Models;

public partial class Examresult
{
    public int? ResultId { get; set; }

    public string StudentId { get; set; } = null!;

    public int ExamId { get; set; }

    public float Grade { get; set; }

    public string? Errors { get; set; }

    public virtual Exam? Exam { get; set; } = null;

    public virtual Student? Student { get; set; } = null;
}
