using System;
using System.Collections.Generic;

namespace exam.Models;

public partial class Exam
{
    public int? ExamId { get; set; }

    public string ExamName { get; set; } = null!;

    public string TeacherId { get; set; } = null!;

    public DateOnly? ExamDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public int? Duration { get; set; }

    public bool Randomized { get; set; }

    public int QuestionCount { get; set; }

    public virtual ICollection<Examresult> Examresults { get; } = new List<Examresult>();

    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    public virtual Teacher? Teacher { get; set; }

}
