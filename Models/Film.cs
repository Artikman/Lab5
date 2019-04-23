using System;
using System.ComponentModel.DataAnnotations;

namespace Lab_4.Models
{
    public class Film
    {
        [Required]
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime Duration { get; set; }
        public string FilmCompany { get; set; }
        public string ProducingCountry { get; set; }
        public string ListOfMainActros { get; set; }
        public int AgeRestrictions { get; set; }
        public string Description { get; set; }
        public int GenreId { get; set; }
        public int SessionId { get; set; }
        public virtual Session Sessions { get; set; }
        public virtual Genre Genres { get; set; }
    }
}