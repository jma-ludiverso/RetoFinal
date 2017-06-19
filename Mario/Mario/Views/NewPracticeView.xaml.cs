using Mario.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mario.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPracticeView : ContentPage
    {

        public object Parameter { get; set; }

        public NewPracticeView(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = new NewPracticeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as NewPracticeViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}