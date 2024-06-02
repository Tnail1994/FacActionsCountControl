using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacActionsCountControl.Control.Models;
using FacActionsCountControl.Control.Services;

namespace FacActionsCountControl.Control.ViewModels
{
	internal interface IControlViewModel
	{
		IActionsCountModel ActionsCount { get; }
		IOverviewModel Overview { get; }

		IRelayCommand LoadedCommand { get; }
	}

	internal partial class ControlViewModel : ObservableObject, IControlViewModel
	{
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
			StartPlayerTime();
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