using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Models
{
    public class Exam
    {
        public int ExamId { get; set; }

        public string ExamName { get; set; } 

        public string TeacherId { get; set; }

        public string ExamDate { get; set; }

        public string StartTime { get; set; }

        public int Duration { get; set; }

        public bool Randomized { get; set; }

        public int QuestionCount { get; set; }

    }
}
