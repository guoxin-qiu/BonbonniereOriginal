using System;
using System.Collections.Generic;

namespace Bonbonniere.Core.EnglishClass.Model
{
    public class Exam
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }

        public List<Enrollment> Enrollments { get; set; }
    }

    public class ScoringExam : Exam
    {
        public decimal TotalScore { get; set; }
    }

    public class UnscoringExam: Exam
    {
        public string Evaluation { get; set; }
    }
}
