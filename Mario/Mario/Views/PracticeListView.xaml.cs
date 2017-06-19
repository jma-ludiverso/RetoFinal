using Mario.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mario.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeListView : ContentPage
    {

        public object Parameter { get; set; }

        public PracticeListView(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = new PracticeListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as PracticeListViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }

    }
}