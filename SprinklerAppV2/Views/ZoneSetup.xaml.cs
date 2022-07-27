using SprinklerAppV2.Models;
using SprinklerAppV2.ViewModels;

namespace SprinklerAppV2.Views;

public partial class ZoneSetup : ContentPage
{
	ZoneSetupViewModel _viewModel;


    public ZoneSetup(ZoneSetupViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		this.Loaded += async (x,y) => await viewModel.OnLoaded();
		_viewModel = viewModel;
	}

	private async void RemoveButton_Clicked(object sender, EventArgs e)
	{
		var button  = sender as Button;
		await _viewModel.RemoveZone(Guid.Parse(button.AutomationId));
	}

	
}