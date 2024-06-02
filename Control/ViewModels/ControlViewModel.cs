using System.Diagnostics;
using System.Threading.Channels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacActionsCountControl.Control.Models;
using FacActionsCountControl.Control.Services;
using FacActionsCountControl.Services;

namespace FacActionsCountControl.Control.ViewModels
{
	internal interface IControlViewModel
	{
		LocalizationResourceManager LocalizationResourceManager { get; }
		int Rotation { get; }
		bool OverlayIsVisible { get; }
		IActionsCountModel ActionsCount { get; }
		IOverviewModel Overview { get; }

		ImageSource PauseResumeImageSource { get; }
		IRelayCommand LoadedCommand { get; }
		IRelayCommand UnloadedCommand { get; }
		IRelayCommand NextPlayerCommand { get; }
		IRelayCommand StartControlCommand { get; }
		IRelayCommand PauseResumeCommand { get; }
		IRelayCommand StopCommand { get; }
	}

	internal partial class ControlViewModel : ObservableObject, IControlViewModel
	{
		[ObservableProperty] private int _rotation;
		[ObservableProperty] private bool _overlayIsVisible;
		[ObservableProperty] private ImageSource _pauseResumeImageSource;

		private readonly ITimeService _timeService;
		private readonly IPlayerService _playerService;
		private readonly IActionsCountService _actionsCountService;

		private readonly IDispatcherTimer _timer;
		private bool _init;

		public ControlViewModel(ITimeService timeService, IPlayerService playerService,
			IActionsCountService actionsCountService)
		{
			_timeService = timeService;
			_playerService = playerService;
			_actionsCountService = actionsCountService;

			if (Application.Current != null)
			{
				_timer = Application.Current.Dispatcher.CreateTimer();
				_timer.Interval = TimeSpan.FromSeconds(1);
				_timer.Tick += (s, e) => RaisePlayerTime();
			}
		}

		public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
		public IActionsCountModel ActionsCount { get; private set; } = ActionsCountModel.Default;
		public IOverviewModel Overview { get; private set; } = OverviewModel.Default;


		[RelayCommand]
		public async void Loaded()
		{
			if (_init && !OverlayIsVisible)
			{
				bool answer =
					await Application.Current.MainPage.DisplayAlert("Action needed", "Do you want to continue?", "Yes",
						"No");

				if (answer)
				{
					StartTimer();
					return;
				}
			}

			Init();
		}

		private void Init()
		{
			InitLayout();
			InitOverview();

			_init = true;
		}

		[RelayCommand]
		public void Unloaded()
		{
			StopTimer();
		}

		[RelayCommand]
		public void PauseResume()
		{
			if (_timer.IsRunning)
				StopTimer();
			else
				StartTimer();
		}

		[RelayCommand]
		public async void Stop()
		{
			bool answer =
				await Application.Current.MainPage.DisplayAlert("Action needed", "Do you want to stop?", "Yes",
					"No");

			if (!answer)
				return;

			Init();
		}

		private void StopTimer()
		{
			_timer.Stop();
			PauseResumeImageSource = "play.png";
		}

		[RelayCommand]
		public void NextPlayer()
		{
			UpdateLayout();
			UpdateOverview();
		}

		[RelayCommand]
		public void StartControl()
		{
			OverlayIsVisible = false;
			StartTimer();
		}

		private void InitLayout()
		{
			Rotation = 0;
			OverlayIsVisible = true;
		}

		private void InitOverview()
		{
			if (_timer.IsRunning)
				StopTimer();

			_actionsCountService.Reset();

			Overview.CurrentPlayerName = _playerService.GetFirstPlayerName();
			Overview.Turn = 1;
			Overview.PlayerTime = _timeService.ResetTime().ToString();
			Overview.NextAction = _actionsCountService.GetNextAction().ToString();

			var defaultActionsCount = ActionsCountModel.Default;

			ActionsCount.Draw = defaultActionsCount.Draw;
			ActionsCount.Summon = defaultActionsCount.Summon;
			ActionsCount.Spell = defaultActionsCount.Spell;
			ActionsCount.Buy = defaultActionsCount.Buy;
			ActionsCount.Attach = defaultActionsCount.Attach;
			ActionsCount.Attack = defaultActionsCount.Attack;
		}

		private void StartTimer()
		{
			_timer.Start();
			PauseResumeImageSource = "pause.png";
		}

		private void RaisePlayerTime()
		{
			Overview.PlayerTime = _timeService.RaisePlayerTime().ToString();
		}

		private void UpdateLayout()
		{
			Rotation += 180;
		}

		private void UpdateOverview()
		{
			Overview.CurrentPlayerName = _playerService.GetNextPlayerName(Overview.CurrentPlayerName);
			Overview.PlayerTime = _timeService.GetTime().ToString();
			Overview.Turn++;

			if (Overview.Turn % 5 == 0)
			{
				switch (_actionsCountService.GetCurrentAction())
				{
					case ActionsCountType.Draw:
						ActionsCount.Draw++;
						break;
					case ActionsCountType.Summon:
						ActionsCount.Summon++;
						break;
					case ActionsCountType.Spell:
						ActionsCount.Spell++;
						break;
					case ActionsCountType.Buy:
						ActionsCount.Buy++;
						break;
					case ActionsCountType.Attack:
						ActionsCount.Attack++;
						break;
					case ActionsCountType.Attach:
						ActionsCount.Attach++;
						break;
				}

				Overview.NextAction = _actionsCountService.GetNextAction().ToString();
			}
		}
	}
}