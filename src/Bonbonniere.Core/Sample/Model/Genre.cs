﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Core.Sample.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Album> Albums { get; set; }
    }
}
