﻿using System.ComponentModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using FacActionsCountControl.Resources.Languages;

namespace FacActionsCountControl.Settings.ViewModels
{
	internal interface ISettingsViewModel
	{
		LocalizationResourceManager LocalizationResourceManager { get; }
		List<string> Languages { get; }
		string CurrentLanguage { get; set; }
	}

	internal partial class SettingsViewModel : ObservableObject, ISettingsViewModel
	{
		[ObservableProperty] private string _currentLanguage;
		[ObservableProperty] private List<string> _languages = new List<string> { "Deutsch", "English" };

		public SettingsViewModel()
		{
			PropertyChanged += OnPropertyChanged;
			CurrentLanguage = MapToLanguage(LocalizationResourceManager.GetCurrentCultureInfo());
		}

		private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(CurrentLanguage))
			{
				LocalizationResourceManager.SetCulture(MapToCultureInfo(CurrentLanguage));
			}
		}

		private CultureInfo MapToCultureInfo(string currentLanguage)
		{
			return currentLanguage switch
			{
				"Deutsch" => new CultureInfo("de-DE"),
				"English" => new CultureInfo("en-US"),
				_ => CultureInfo.CurrentCulture
			};
		}

		// Create me the opposite of the method above MapToCultureInfo
		private string MapToLanguage(CultureInfo currentCultureInfo)
		{
			return currentCultureInfo switch
			{
				CultureInfo { Name: "de-DE" } => "Deutsch",
				CultureInfo { Name: "en-US" } => "English",
				_ => "English"
			};
		}


		public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
	}
}