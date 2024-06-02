using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacActionsCountControl.Control.Models;
using FacActionsCountControl.Control.Services;
using FacActionsCountControl.Services;

namespace FacActionsCountControl.Control.ViewModels
{
	internal interface IControlViewModel
	{
		int Rotation { get; }
		bool OverlayIsVisible { get; }
		IActionsCountModel ActionsCount { get; }
		IOverviewModel Overview { get; }

		IRelayCommand LoadedCommand { get; }
		IRelayCommand NextPlayerCommand { get; }
		IRelayCommand StartControlCommand { get; }
	}

	internal partial class ControlViewModel : ObservableObject, IControlViewModel
	{
		[ObservableProperty] private int _rotation;
		[ObservableProperty] private bool _overlayIsVisible;

		private readonly ITimeService _timeService;
		private readonly IPlayerService _playerService;

		public ControlViewModel(ITimeService timeService, IPlayerService playerService)
		{
			_timeService = timeService;
			_playerService = playerService;
		}

		public IActionsCountModel ActionsCount { get; } = ActionsCountModel.Default;
		public IOverviewModel Overview { get; } = OverviewModel.Default;


		[RelayCommand]
		public void Loaded()
		{
			InitLayout();
			InitOverview();
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
			StartPlayerTime();
		}

		private void InitLayout()
		{
			Rotation = 0;
			OverlayIsVisible = true;
		}

		private void InitOverview()
		{
			Overview.CurrentPlayerName = _playerService.GetFirstPlayerName();
			Overview.Turn = 1;
		}

		private void StartPlayerTime()
		{
			var timer = new System.Threading.Timer(obj => { RaisePlayerTime(); }, null,
				TimeSpan.FromSeconds(1),
				TimeSpan.FromSeconds(1));
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
			}
		}
	}
}