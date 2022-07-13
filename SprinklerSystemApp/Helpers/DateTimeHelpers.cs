using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprinklerSystemApp.Helpers
{
    public static class DateTimeHelpers
    {
        public static readonly DateTime MinimumDate = DateTime.Now;
        public static readonly DateTime MaximumDate = DateTime.Now.AddDays(7);

    }
}
