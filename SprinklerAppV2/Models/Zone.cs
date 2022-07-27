using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprinklerAppV2.Models
{
    public class Zone
    {
        public Guid Id { get;  init; } = Guid.NewGuid();
        public string Name { get; set; }
        public int ZoneId { get; set; }
    }
}
