namespace FacActionsCountControl.Control.Views;

public partial class KeyValueView : ContentView
{
	public KeyValueView()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty KeyProperty =
		BindableProperty.Create(nameof(Key), typeof(string), typeof(KeyValueView), default(string));

	public string Key
	{
		get => (string)GetValue(KeyProperty);
		set => SetValue(KeyProperty, value);
	}
}