namespace FacActionsCountControl.Control.Views;

public partial class KeyValueView : ContentView
{
	public KeyValueView()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty KeyProperty =
		BindableProperty.Create(nameof(Key), typeof(string), typeof(KeyValueView), "Key");

	public string Key
	{
		get => (string)GetValue(KeyProperty);
		set => SetValue(KeyProperty, value);
	}

	public static readonly BindableProperty ValueProperty =
		BindableProperty.Create(nameof(Value), typeof(string), typeof(KeyValueView), "Value");

	public string Value
	{
		get => (string)GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	public static readonly BindableProperty ValueFontSizeProperty =
		BindableProperty.Create(nameof(ValueFontSize), typeof(int), typeof(KeyValueView), 28);

	public int ValueFontSize
	{
		get => (int)GetValue(ValueFontSizeProperty);
		set => SetValue(ValueFontSizeProperty, value);
	}

	public static readonly BindableProperty KeyFontSizeProperty =
		BindableProperty.Create(nameof(KeyFontSize), typeof(int), typeof(KeyValueView), 14);

	public int KeyFontSize
	{
		get => (int)GetValue(KeyFontSizeProperty);
		set => SetValue(KeyFontSizeProperty, value);
	}
}