using System;
using System.Collections.Generic;

namespace Bonbonniere.Core.Models
{
    public class BrainstormSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual List<Idea> Ideas { get; set; }

        public BrainstormSession()
        {
            Ideas = new List<Idea>();
        }

        public void AddIdea(Idea idea)
        {
            Ideas.Add(idea);
        }
    }

    public class Idea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual BrainstormSession BrainstormSession { get; set; }
    }
}
