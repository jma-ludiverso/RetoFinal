using Mario.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mario.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticingView : ContentPage
    {

        public object Parameter { get; set; }

        public PracticingView(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = new PracticingViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as PracticingViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}