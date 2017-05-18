using Bonbonniere.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Core.Models.MusicStore
{
    public class Album : IAggregateRoot
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Range(0.01, 100.00)]
        [DataType(DataType.Currency)] // TODO: add validation
        public decimal Price { get; set; }
        public string ArtUrl { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public virtual Genre Genre { get; set; }

        public Album()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
