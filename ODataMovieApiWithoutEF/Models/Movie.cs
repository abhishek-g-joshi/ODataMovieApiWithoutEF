﻿using System.ComponentModel.DataAnnotations;

namespace ODataMovieApiWithoutEF.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Diector { get; set; }
    }
}
