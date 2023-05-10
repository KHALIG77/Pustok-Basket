﻿using System.ComponentModel.DataAnnotations;

namespace PustokStart.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
