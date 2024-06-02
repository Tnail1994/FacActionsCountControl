namespace FacActionsCountControl.Control.Services
{
	internal interface IPlayerTimeService
	{
		TimeSpan RaisePlayerTime(string overviewCurrentPlayer);
	}

	internal class PlayerTimeService : IPlayerTimeService
	{
		private readonly Dictionary<string, TimeSpan> _playerTimes = new();

		public TimeSpan RaisePlayerTime(string playerName)
		{
			if (_playerTimes.ContainsKey(playerName))
				_playerTimes[playerName] += TimeSpan.FromSeconds(1);
			else
				_playerTimes.Add(playerName, TimeSpan.FromSeconds(0));

			return _playerTimes[playerName];
		}
	}
}