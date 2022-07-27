using SprinklerAppV2.ViewModels;

namespace SprinklerAppV2
{
    public partial class MainPage : ContentPage
    {
     

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }

       
    }
}