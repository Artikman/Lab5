using Lab_4.Models;
using System.Collections.Generic;

namespace Lab_4.ViewModels
{
    public class PlaceViewModel
    {
        public List<Place> places { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}