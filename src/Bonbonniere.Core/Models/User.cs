using Bonbonniere.Infrastructure.Domain;

namespace Bonbonniere.Core.Models
{
    public class User : IAggregateRoot
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string FullName
        {
            get
            {
                return $"{UserProfile.FirstName} {UserProfile.LastName}";
            }
        }

        public virtual UserProfile UserProfile { get; set; }

        public User()
        {
            UserProfile = new UserProfile();
        }
    }
}