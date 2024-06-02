using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacActionsCountControl.Control.Models;
using FacActionsCountControl.Control.Services;

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

		public ControlViewModel(IPlayerTimeService playerTimeService)
		{
			_playerTimeService = playerTimeService;
		}

		public IActionsCountModel ActionsCount { get; } = ActionsCountModel.Default;
		public IOverviewModel Overview { get; } = OverviewModel.Default;


		[RelayCommand]
		public void Loaded()
		{
			Overview.CurrentPlayer = "Player 1";
			StartPlayerTime();
		}

		[RelayCommand]
		public void NextPlayer()
		{
			SetLayout();
			SetOverview();
		}

		private void SetLayout()
		{
			Rotation += 180;
		}

		private void SetOverview()
		{
			Overview.CurrentPlayer = Overview.CurrentPlayer == "Player 1" ? "Player 2" : "Player 1";
			Overview.PlayerTime = _playerTimeService.GetPlayerTime(Overview.CurrentPlayer).ToString();
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
			Overview.PlayerTime = _playerTimeService.RaisePlayerTime(Overview.CurrentPlayer).ToString();
		}
	}
}