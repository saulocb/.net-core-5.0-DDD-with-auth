using System;
using System.Collections.Generic;
using System.Text;

namespace Bouncer.ViewModels.AppObject
{
    public class Shift_vw : EntityBase_vw<long>
    {
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int TotalBouncerty { get; set; }
        public int Interval { get; set; }
    }
}
