using SprinklerSystemApp.ViewModels;

namespace SprinklerSystemApp.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}

