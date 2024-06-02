using CommunityToolkit.Mvvm.ComponentModel;

namespace FacActionsCountControl.Control.Models
{
	internal interface IOverviewModel
	{
		string NextAction { get; set; }
		string PlayerTime { get; set; }
		string CurrentPlayer { get; set; }
		int Turn { get; set; }
	}

	internal partial class OverviewModel : ObservableObject, IOverviewModel
	{
		[ObservableProperty] private string _nextAction, _playerTime, _currentPlayer;
		[ObservableProperty] private int _turn;

		public static IOverviewModel Default => new OverviewModel
		{
			NextAction = "-",
			PlayerTime = "00:00:00",
			CurrentPlayer = "-",
			Turn = 0
		};
	}
}