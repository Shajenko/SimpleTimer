using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Windows.Input;

namespace Simple_Timer
{
    class TimerModel
    {
        public double Seconds { get; internal set; }
        public long Minutes { get; internal set; }

        private bool _running = false;

        private DateTime _last, _current;

        public TimerModel()
        {
            Seconds = 0.0;
            Minutes = 0;
            _last = DateTime.Now;
            _current = DateTime.Now;
        }

        public void ToggleRunning()
        {
            _running = !_running;
            if(_running)
            {
                _last = DateTime.Now;
                _current = DateTime.Now;
            }
            return;
        }

        public void Reset()
        {
            _running = false;
            Seconds = 0.0;
            Minutes = 0;
        }

        public void Run()
        {
            if (!_running)
                Thread.Sleep(1);
            else
            {
                double delta;
                _current = DateTime.Now;
                delta = Convert.ToDouble(_current - _last);
                Seconds += delta;
                if(Seconds >= 60.0)
                {
                    Seconds -= 60.0;
                    Minutes++;
                }

                Thread.Sleep(1);
                
            }
        }
    }
}
