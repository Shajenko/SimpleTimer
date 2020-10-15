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

        public bool Running { get { return _running; } }

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
            Console.WriteLine("Creating TimerModel");
        }

        public void ToggleRunning()
        {
            // make sure to avoid threading issues

            TimeSpan delta;
            if(!_running)
            {
                Console.WriteLine("Restarting timer");
                _currentTime = DateTime.Now;
                _previousTime = _currentTime;
                delta = (_currentTime - _previousTime);
                Console.WriteLine("delta.TotalMilliseconds: " + $"{delta.TotalMilliseconds,1:f}");
                // UpdateTime called in MainWindowVM, no need to call it here
                _running = true;
            }
            else
            {
                _running = false;
                Console.WriteLine("Pausing Timer");
                _currentTime = DateTime.Now;
                _previousTime = _currentTime;
                delta = (_currentTime - _previousTime);
            }
        }

        public void Reset()
        {
            _running = false;
            Milliseconds = 0.0;
            Seconds = 0;
            Minutes = 0;
            _currentTime = DateTime.Now;
            _previousTime = _currentTime;
        }

        public bool UpdateTime()
        {
            double doubRemainder;
            long longRemainder;
            long dividend = 0;
            if (_running)
            {
                TimeSpan delta;
                _currentTime = DateTime.Now;
                delta = (_currentTime - _previousTime);
                Milliseconds += delta.TotalMilliseconds;

                if (delta.TotalMilliseconds > 2000.0)
                {
                    Console.WriteLine("Too many milliseconds.\n");
                    Console.WriteLine("Milliseconds: " + $"{delta.TotalMilliseconds,2:f}" + "\n");
                    throw new Exception("Too many milliseconds");  // Exception thrown after toggle, bug somewhere
                }
                if (Milliseconds >= 1000.0)
                {
                    doubRemainder = Milliseconds % 1000.0;
                    dividend = Convert.ToInt64(Milliseconds / 1000);
                    Milliseconds = doubRemainder;
                    Seconds += dividend;
                }
                if (Seconds >= 120)
                    throw new Exception("Too many seconds");

                if(Seconds >= 60)
                {
                    longRemainder = Seconds % 60;
                    Seconds = longRemainder;
                    Minutes++;
                }
                if (delta.TotalMilliseconds < 1.0)
                {
                    Thread.Sleep(1);
                }


                _previousTime = _currentTime;
                return true;
            }

            return false;
        }
    }
}
