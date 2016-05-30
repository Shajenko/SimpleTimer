using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Timer
{
    class MainWindowMV
    {
        private TimerModel tMod = new TimerModel();

        public string Seconds
        {
            get { return tMod.Seconds.ToString(); }
            set { tMod.Seconds = Convert.ToDouble(value); }
        }

        public string Minutes
        {
            get { return tMod.Minutes.ToString(); }
            set { tMod.Minutes = Convert.ToInt64(value); }
        }



    }
}
