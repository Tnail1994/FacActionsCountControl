using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacActionsCountControl.Resources.Languages;

namespace FacActionsCountControl
{
	public class LocalizationResourceManager : INotifyPropertyChanged
	{
		private LocalizationResourceManager()
		{
			AppResources.Culture = CultureInfo.CurrentCulture;
		}

		public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

		public event PropertyChangedEventHandler PropertyChanged;

		public object this[string key] => AppResources.ResourceManager.GetString(key, AppResources.Culture);

		public void SetCulture(CultureInfo culture)
		{
			AppResources.Culture = culture;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}
}