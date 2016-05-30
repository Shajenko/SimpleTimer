using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Timer
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private TimerModel tMod = new TimerModel();

        private bool _running = false;
        private System.Threading.Thread _timerThread;

        public MainWindowVM()
        {
            _timerThread =  new System.Threading.Thread(tMod.Run);
            _timerThread.Start();
        }

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

        public void ToggleRunning()
        {
            tMod.ToggleRunning();
            RaisePropertyChanged("Seconds");
            RaisePropertyChanged("Minutes");
        }

        public void Reset()
        {
            tMod.Reset();
            RaisePropertyChanged("Seconds");
            RaisePropertyChanged("Minutes");
        }

/*        public void Run()
        {
            tMod.Run();
            RaisePropertyChanged("Seconds");
            RaisePropertyChanged("Minutes");
        }*/

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


    }
}
