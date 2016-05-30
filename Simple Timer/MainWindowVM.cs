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

        public string Seconds
        {
            get { return tMod.Seconds.ToString(); }
            set { tMod.Seconds = Convert.ToDouble(value);
                RaisePropertyChanged("Seconds");
            }
        }

        public string Minutes
        {
            get { return tMod.Minutes.ToString(); }
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
