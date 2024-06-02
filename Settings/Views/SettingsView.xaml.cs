using FacActionsCountControl.Settings.ViewModels;

namespace FacActionsCountControl.Settings.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
		BindingContext =
			Application.Current?.MainPage?.Handler?.MauiContext?.Services.GetService<ISettingsViewModel>();
	}
}