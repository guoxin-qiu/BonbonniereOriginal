using Bonbonniere.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Core.Models.MusicStore
{
    public class Album : IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [Range(0.01, 100.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [StringLength(1024)]
        public string ArtUrl { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        public Album()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
