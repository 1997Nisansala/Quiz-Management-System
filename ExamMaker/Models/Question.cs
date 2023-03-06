using System;
using System.Collections.Generic;

namespace exam.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int ExamId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string Option1 { get; set; } = null!;

    public string Option2 { get; set; } = null!;

    public string Option3 { get; set; } = null!;

    public string Option4 { get; set; } = null!;

    public sbyte CorrectOption { get; set; }

    public virtual Exam Exam { get; set; } = null!;
}
