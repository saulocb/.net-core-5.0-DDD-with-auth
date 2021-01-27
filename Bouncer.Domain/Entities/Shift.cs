using System;
using System.Collections.Generic;
using System.Text;

namespace Bouncer.Domain.Entities
{
    public class Shift: EntityBase<long>
    {
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public int TotalBouncer { get; set; }
        public int Interval { get; set; }
    }
}
