using System;
using System.Collections.Generic;

namespace Lab_4.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime EndTime { get; set; }
        public float TicketPrice { get; set; }
        public string EmployessInvolvedInTheSession { get; set; }
        public int PlaceId { get; set; }
        public Place Places { get; set; }
        public virtual ICollection<Film> Films { get; set; }
    }
}