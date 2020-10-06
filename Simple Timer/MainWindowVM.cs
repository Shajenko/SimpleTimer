using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simple_Timer
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private TimerModel tMod = new TimerModel();

        private System.Threading.Thread _timerThread;

        public MainWindowVM()
        {
            _timerThread =  new System.Threading.Thread(Run);
            _timerThread.Start();
        }

        ~MainWindowVM()
        {
            _timerThread.Abort();
        }

        public string Milliseconds
        {
            get { return tMod.Milliseconds.ToString("000"); }
            set
            {
                tMod.Milliseconds = (long)(Convert.ToUInt64(value));
                RaisePropertyChanged("Milliseconds");
            }
        }

        public string Seconds
        {
            get { return tMod.Seconds.ToString("00"); }
            set { tMod.Seconds = Convert.ToInt64(value);
                RaisePropertyChanged("Seconds");
            }
        }

        public string Minutes
        {
            get { return tMod.Minutes.ToString("00"); }
            set { tMod.Minutes = Convert.ToInt64(value);
                RaisePropertyChanged("Minutes");
            }
        }

        public void Run()
        {
            while(true)
            {
                if(tMod.UpdateTime())
                {
                    RaisePropertyChanged("Milliseconds");
                    RaisePropertyChanged("Seconds");
                    RaisePropertyChanged("Minutes");
                }
            }
        }

        #region ICommands

        void ToggleRunningExecute()
        {
            tMod.ToggleRunning();
        }
  
        bool CanToggleRunningExecute()
        {
            return true;
        }
  
        public ICommand ToggleRunning
        {
            get { return new RelayCommand(ToggleRunningExecute, CanToggleRunningExecute); }
        }

        void ResetExecute()
        {
            tMod.Reset();
            RaisePropertyChanged("Milliseconds");
            RaisePropertyChanged("Seconds");
            RaisePropertyChanged("Minutes");
        }

        bool CanResetExecute()
        {
            return true;
        }

        public ICommand Reset
        {
            get { return new RelayCommand(ResetExecute, CanResetExecute); }
        }
        #endregion


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
