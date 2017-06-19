using Mario.Models;
using Mario.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mario.ViewModels
{
    class PracticingViewModel : ViewModelBase
    {

        private ObservableCollection<PracticasDetalle> vDetails;
        ClockService clock = null;
        string _runningTime;
        int _exercisenumber;
        Practica _Practica;
        private bool _isBusy;
        private bool _ending;
        private bool _isPaused;

        public ICommand BorrarCommand => new Command(DeletePractice);
        public ICommand EndPracticeCommand => new Command(EndPractice);
        public ICommand NewExerciseCommand => new Command(NewExercise);
        public ICommand PauseCommand => new Command(Pause);
        public ICommand VolverCommand => new Command(Back);

        public ObservableCollection<PracticasDetalle> Exercises
        {
            get { return vDetails; }
            set
            {
                vDetails = value;
                OnPropertyChanged("Exercises");
            }
        }

        public string LastNumber
        {
            get { return _exercisenumber.ToString(); }
        }

        public string RunningTime
        {
            get { return _runningTime; }
            set
            {
                _runningTime = value;
                OnPropertyChanged("RunningTime");
            }
        }

        public bool TimerVisible
        {
            get
            {
                if (clock == null)
                {
                    return false;
                }else
                {
                    return true;
                }
            }
        }

        private void Back()
        {
            NavigationService.Instance.NavigateTo<PracticeListViewModel>();
        }

        private async void DeletePractice()
        {
            try
            {
                await MarioService.Instance.DeletePractice(_Practica);
                this.Back();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }
        }

        private async void EndPractice()
        {
            try
            {
                _ending = true;
                this.NewExercise();
                clock = null;
                OnPropertyChanged("TimerVisible");
                int resultOk = 0;
                for(int i=0; i <= vDetails.Count - 1; i++)
                {
                    if (vDetails[i].Resultado)
                    {
                        resultOk += 1;
                    }
                }
                double percentage = (resultOk * 100) / vDetails.Count;
                _Practica.Resultado = Math.Round(percentage, 2);
                await MarioService.Instance.EditPractice(_Practica);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }
        }

        private async void LoadDetails()
        {
            if (!_isBusy)
            {
                _isBusy = true;
                var items = await MarioService.Instance.GetDetailItems(_Practica.id);
                if (items != null)
                {
                    Exercises = new ObservableCollection<PracticasDetalle>(items);
                }
                _isBusy = false;
            }
        }

        private async void NewExercise()
        {
            try
            {
                PracticasDetalle exercise = new PracticasDetalle();
                exercise.IdPractica = _Practica.id;
                exercise.Numero = _exercisenumber;
                exercise.Minutos = (clock.MinutesRunning * 60) + clock.SecondsRunning;
                if(exercise.Minutos<=(_Practica.Minutos * 60))
                {
                    exercise.Resultado = true;
                }
                else
                {
                    exercise.Resultado = false;
                }
                clock.Stop();
                await MarioService.Instance.AddPracticeExercise(exercise);
                vDetails.Add(exercise);
                OnPropertyChanged("Exercises");
                if (!_ending)
                {
                    this.RunningTime = "00:00";
                    clock.Start();
                    _exercisenumber += 1;
                    OnPropertyChanged("LastNumber");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            _Practica = (Practica)navigationContext;

            if (string.IsNullOrEmpty(_Practica.id))
            {
                await MarioService.Instance.AddPractice(_Practica);
                clock = new ClockService(5);
                clock.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
                {
                    setRunningTime();
                };
                clock.Start();
                _exercisenumber = 1;
                OnPropertyChanged("LastNumber");
                vDetails = new ObservableCollection<PracticasDetalle>();
            }
            else
            {
                this.LoadDetails();
            }
            OnPropertyChanged("TimerVisible");
        }

        private void Pause()
        {
            if (!_isPaused)
            {
                clock.Pause();
            }
            else
            {
                clock.Restart();
            }
            _isPaused = !_isPaused;
        }

        void setRunningTime()
        {
            int seconds = clock.SecondsRunning;
            int minutes = clock.MinutesRunning;
            seconds = seconds - (minutes * 60);
            this.RunningTime = string.Format("{0}:{1}",minutes.ToString("D2"),seconds.ToString("D2"));
        }
    }
}
