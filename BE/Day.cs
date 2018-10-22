using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Day
    {
        public Time start;
        public Time end;

        public Day(Time start, Time end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
