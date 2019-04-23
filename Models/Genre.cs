using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4.Models
{
    public class Genre
    {
        [Required]
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public Genre()
        {
            Films = new List<Film>();
        }
    }
}