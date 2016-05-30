using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Timer
{
    class TimerModel
    {
        public double Seconds { get; internal set; }
        public long Minutes { get; internal set; }

        private double _seconds;
        private long _minutes;
    }
}
