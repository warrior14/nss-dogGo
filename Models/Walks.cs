using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Walks
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ParsedDateTime
        {
            get
            {
                return Date.ToShortDateString();
            }
        }
        public int Duration { get; set; }
        public int DurationInMinutes
        {
            get
            {
                return Duration / 60;
            }
        }
        public int TotalWalkTime { get; set; }
        public int WalkerId { get; set; }
        public int DogId { get; set; }
        public Walker Walker { get; set; }
        public Dog Dog { get; set; }
        public Owner Owner { get; set; }

    }
}
