using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simple_Timer
{
    class TimerModel
    {
        public double Seconds { get; internal set; }
        public long Minutes { get; internal set; }

        private bool _running = false;
        private DateTime _previousTime, _currentTime;

        public TimerModel()
        {
            _running = false;
            Seconds = 0.0;
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
            }
        }

        public void Reset()
        {
            _running = false;
            Seconds = 0.0;
            Minutes = 0;
        }

        public bool UpdateTime()
        {
            if (_running)
            {
                TimeSpan delta;
                _currentTime = DateTime.Now;
                delta = (_currentTime - _previousTime);
                Seconds += delta.TotalSeconds;
                if (Seconds >= 60.0)
                {
                    Seconds -= 60.0;
                    Minutes++;
                }
                _previousTime = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
