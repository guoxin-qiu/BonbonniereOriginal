using System;
using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Website.Features.MusicStore
{
    public class AlbumsViewModel
    {
        [Display(Name = "Album Id")]
        public int AlbumId { get; set; }
        [Display(Name = "Title")]
        public string AlbumTitle { get; set; }
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal AlbumPrice { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AlbumReleaseDate { get; set; }
        [Display(Name = "Art Url")]
        public string AlbumArtUrl { get; set; }
    }
}
