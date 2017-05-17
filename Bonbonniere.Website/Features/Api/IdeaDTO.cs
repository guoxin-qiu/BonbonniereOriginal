using System;

namespace Bonbonniere.Website.Features.Api
{
    public class IdeaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
