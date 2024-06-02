using CommunityToolkit.Maui;
using FacActionsCountControl.Control.Services;
using FacActionsCountControl.Control.ViewModels;
using FacActionsCountControl.Services;
using FacActionsCountControl.Settings.ViewModels;
using Microsoft.Extensions.Logging;

namespace FacActionsCountControl
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			// https://learn.microsoft.com/de-de/dotnet/architecture/maui/dependency-injection                                                                                    
			builder.RegisterAppServices();
			builder.RegisterViewModels();
			builder.UseMauiCommunityToolkit();
			return builder.Build();
		}

		public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
		{
			mauiAppBuilder.Services.AddSingleton<IControlViewModel, ControlViewModel>();
			mauiAppBuilder.Services.AddSingleton<ISettingsViewModel, SettingsViewModel>();

			return mauiAppBuilder;
		}

		public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
		{
			mauiAppBuilder.Services.AddSingleton<IPlayerTimeService, PlayerTimeService>();
			mauiAppBuilder.Services.AddSingleton<IPlayerService, PlayerService>();


			return mauiAppBuilder;
		}
	}
}