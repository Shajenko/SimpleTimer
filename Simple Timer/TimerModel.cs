using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simple_Timer
{
    class TimerModel
    {
        public double Milliseconds { get; internal set; }
        public long Seconds { get; internal set; }
        public long Minutes { get; internal set; }

        private bool _running = false;
        private DateTime _previousTime, _currentTime;

        public TimerModel()
        {
            _running = false;
            Milliseconds = 0.0;
            Seconds = 0;
            Minutes = 0;
            _previousTime = DateTime.Now;
            _currentTime = DateTime.Now;
        }

        public void ToggleRunning()
        {
            _running = !_running;
            if(_running)
            {
                _previousTime = DateTime.Now;
                _currentTime = DateTime.Now;
                UpdateTime();
            }
        }

        public void Reset()
        {
            _running = false;
            Milliseconds = 0.0;
            Seconds = 0;
            Minutes = 0;
            UpdateTime();
        }

        public bool UpdateTime()
        {
            double doubRemainder;
            long longRemainder;
            long dividend;
            if (_running)
            {
                TimeSpan delta;
                _currentTime = DateTime.Now;
                delta = (_currentTime - _previousTime);
                Milliseconds += delta.TotalMilliseconds;
                if (Milliseconds >= 1000.0)
                {
                    doubRemainder = Milliseconds % 1000.0;
                    dividend = Convert.ToInt64(Milliseconds / 1000);
                    Milliseconds = doubRemainder;
                    Seconds += dividend;
                }

                if(Seconds >= 60)
                {
                    longRemainder = Seconds % 60;
                    dividend = Seconds / 60;
                    Seconds = longRemainder;
                    Minutes += dividend;
                }

                _previousTime = _currentTime;
                return true;
            }
            return false;
        }
    }
}
