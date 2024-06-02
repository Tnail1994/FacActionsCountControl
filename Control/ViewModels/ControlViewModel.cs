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
		IActionsCountModel ActionsCount { get; }
		IOverviewModel Overview { get; }

		IRelayCommand LoadedCommand { get; }
		IRelayCommand NextPlayerCommand { get; }
	}

	internal partial class ControlViewModel : ObservableObject, IControlViewModel
	{
		[ObservableProperty] private int _rotation;
		private readonly IPlayerTimeService _playerTimeService;
		private readonly IPlayerService _playerService;

		public ControlViewModel(IPlayerTimeService playerTimeService, IPlayerService playerService)
		{
			_playerTimeService = playerTimeService;
			_playerService = playerService;
		}

		public IActionsCountModel ActionsCount { get; } = ActionsCountModel.Default;
		public IOverviewModel Overview { get; } = OverviewModel.Default;


		[RelayCommand]
		public void Loaded()
		{
			InitLayout();
			InitOverview();

			// Dummy, move later to start game command
			StartPlayerTime();
		}

		[RelayCommand]
		public void NextPlayer()
		{
			UpdateLayout();
			UpdateOverview();
		}


		private void InitLayout()
		{
			Rotation = 0;
		}

		private void InitOverview()
		{
			Overview.CurrentPlayerName = _playerService.GetFirstPlayerName();
			Overview.Turn = 1;
		}

		private void UpdateLayout()
		{
			Rotation += 180;
		}

		private void UpdateOverview()
		{
			Overview.CurrentPlayerName = _playerService.GetNextPlayerName(Overview.CurrentPlayerName);
			Overview.PlayerTime = _playerTimeService.GetPlayerTime(Overview.CurrentPlayerName).ToString();
			Overview.Turn++;

			if (Overview.Turn % 5 == 0)
			{
			}
		}

		private void StartPlayerTime()
		{
			var timer = new System.Threading.Timer(obj => { RaisePlayerTime(); }, null,
				TimeSpan.FromSeconds(1),
				TimeSpan.FromSeconds(1));
		}

		private void RaisePlayerTime()
		{
			Overview.PlayerTime = _playerTimeService.RaisePlayerTime(Overview.CurrentPlayerName).ToString();
		}
	}
}