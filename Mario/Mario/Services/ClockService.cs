using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Services
{

    public class ClockService : INotifyPropertyChanged
    {
        DateTime startTime;
        int minutes;
        bool _isRunning = false;
        int _secondsRunning;
        int _minutesRunning;
        int _sencondsBeforePause = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public int MinutesRunning
        {
            get { return _minutesRunning; }
        }

        public int SecondsRunning
        {
            get { return this._secondsRunning; }
            protected set
            {
                this._secondsRunning = value;
                OnPropertyChanged("SecondsRunning");
            }
        }

        public ClockService(int minutesToCount)
        {
            minutes = minutesToCount;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Pause()
        {
            _isRunning = false;
            DateTime endTime = DateTime.Now;
            double dblDif = (endTime - startTime).TotalSeconds;
            _sencondsBeforePause = (int)dblDif;
        }

        public void Restart()
        {
            startTime = DateTime.Now.AddSeconds(-_sencondsBeforePause);
            _sencondsBeforePause = 0;
            _isRunning = true;
            Timer();
        }

        public void Start()
        {
            startTime = DateTime.Now;
            _sencondsBeforePause = 0;            
            _isRunning = true;
            Timer();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        async void Timer()
        {
            while (_isRunning)
            {
                await Task.Delay(1000);
                DateTime endTime = DateTime.Now;
                double dblDif = (endTime - startTime).TotalSeconds;
                this.SecondsRunning = (int)dblDif;
                _minutesRunning = ((int)dblDif / 60);
                if (dblDif >= (minutes * 60))
                {
                    _isRunning = false;
                }
            }
        }


    }

}
