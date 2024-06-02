using CommunityToolkit.Mvvm.ComponentModel;

namespace FacActionsCountControl.Control.Models
{
	internal interface IOverviewModel
	{
		string NextValue { get; set; }
		string PlayerTime { get; set; }
		string CurrentPlayer { get; set; }
		int Turn { get; set; }
	}

	internal partial class OverviewModel : ObservableObject, IOverviewModel
	{
		[ObservableProperty] private string _nextValue, _playerTime, _currentPlayer;
		[ObservableProperty] private int _turn;

		public static IOverviewModel Default => new OverviewModel
		{
			NextValue = "-",
			PlayerTime = "00:00:00",
			CurrentPlayer = "Player 1",
			Turn = 0
		};
	}
}