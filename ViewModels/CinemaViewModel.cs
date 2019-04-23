using Lab_4.Models;
using System.Collections.Generic;

namespace Lab_4.ViewModels
{
    public class CinemaViewModel
    {
        public List<Cinema> cinemas { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string Name { get; set; }
    }
}