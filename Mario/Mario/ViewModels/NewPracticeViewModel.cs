using Mario.Models;
using Mario.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mario.ViewModels
{
    class NewPracticeViewModel : ViewModelBase
    {

        private DateTime _Fecha;
        private string _Descripcion;
        private float _Minutos;

        public ICommand BackCommand => new Command(Back);
        public ICommand StartCommand => new Command(Start);

        public string Descripcion
        {
            get { return _Descripcion; }
            set
            {
                _Descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                _Fecha = value;
                OnPropertyChanged("Fecha");
            }
        }

        public float Minutos
        {
            get { return _Minutos; }
            set
            {
                _Minutos = value;
                OnPropertyChanged("Minutos");
            }
        }

        private void Back()
        {
            NavigationService.Instance.NavigateBack();
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            this.Fecha = System.DateTime.Now;
            this.Minutos = 5;
        }

        private void Start()
        {
            var parameter = new Practica();
            parameter.Descripcion = _Descripcion;
            parameter.Fecha = _Fecha;
            parameter.Minutos = _Minutos;
            NavigationService.Instance.NavigateTo<PracticingViewModel>(parameter);
        }

    }
}
