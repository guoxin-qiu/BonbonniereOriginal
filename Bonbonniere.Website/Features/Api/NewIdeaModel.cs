using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Website.Features.Api
{
    public class NewIdeaModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1,1000000)]
        public int SessionId { get; set; }
    }
}
