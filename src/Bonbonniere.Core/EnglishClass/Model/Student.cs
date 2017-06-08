using System.Collections.Generic;

namespace Bonbonniere.Core.EnglishClass.Model
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ChineseName { get; set; }
        public Gender Gender { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public List<Enrollment> Enrollments { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
