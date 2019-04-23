using System;

namespace Lab_4.Models
{
    public class Cinema
    {
        public Cinema() { }

        public Cinema(string name, int age, DateTime duration, int id)
        {
            Name = name;
            Age = age;
            Duration = duration;
            Id = id;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Duration { get; set; }
        public int Id { get; set; }
    }
}