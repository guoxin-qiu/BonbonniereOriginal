namespace Bonbonniere.Core.EnglishClass.Model
{
    public class Enrollment
    {
        public int ID { get; set; }
        public int ExamID { get; set; }
        public int StudentID { get; set; }
        public Grade Grade { get; set; }

        public Exam Exam { get; set; }
        public Student Student { get; set; }
    }
}
