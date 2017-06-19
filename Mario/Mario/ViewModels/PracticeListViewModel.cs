using Mario.Models;
using Mario.Services;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mario.ViewModels
{
    class PracticeListViewModel : ViewModelBase
    {
        private ObservableCollection<Practica> vPractices;
        private Practica vSelectedItem;
        private bool _isBusy;

        public ICommand NewPractice => new Command(New);

        public ObservableCollection<Practica> Practicas
        {
            get { return vPractices; }
            set
            {
                vPractices = value;
                OnPropertyChanged("Practicas");
            }
        }

        public Practica SelectedItem
        {
            get { return vSelectedItem; }
            set
            {
                vSelectedItem = value;
                if (vSelectedItem != null)
                {
                    OnPropertyChanged("SelectedItem");
                    var parameter = vSelectedItem;
                    try
                    {
                        NavigationService.Instance.NavigateTo<PracticingViewModel>(parameter);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(@"Error {0}", ex.Message);
                    }
                    vSelectedItem = null;
                }
            }
        }

        private void New()
        {
            NavigationService.Instance.NavigateTo<NewPracticeViewModel>();
        }

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            if (!_isBusy)
            {
                _isBusy = true;
                var items = await MarioService.Instance.GetItems();
                if (items != null)
                {
                    Practicas = new ObservableCollection<Practica>(new ObservableCollection<Practica>(items).OrderByDescending(p => p.Fecha));
                }
                _isBusy = false;
            }           
        }

    }
}
