namespace FacActionsCountControl
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			BindingContext = this;
		}

		public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
	}
}