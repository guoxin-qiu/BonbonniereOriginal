namespace Bonbonniere.Core.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string IPAddress { get; set; }
        public virtual User User { get; set; }
    }
}
