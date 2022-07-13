using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprinklerSystemApp.Models
{
    public class SprinklerTimeSpan
    {
        public Guid Id { get; init; }
        public DateTime Day { get; set; }
        public TimeSpan BegTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsValid => BegTime.CompareTo(EndTime) < 0;
        
    }
}
