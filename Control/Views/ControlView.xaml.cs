using FacActionsCountControl.Control.ViewModels;

namespace FacActionsCountControl.Control.Views;

public partial class ControlView : ContentPage
{
	public ControlView()
	{
		InitializeComponent();
		BindingContext =
			Application.Current?.MainPage?.Handler?.MauiContext?.Services.GetService<IControlViewModel>();
	}
}