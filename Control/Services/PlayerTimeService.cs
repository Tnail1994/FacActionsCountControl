namespace FacActionsCountControl.Control.Services
{
	internal interface IPlayerTimeService
	{
		TimeSpan GetPlayerTime(string overviewCurrentPlayer);
		TimeSpan RaisePlayerTime(string overviewCurrentPlayer);
	}

	internal class PlayerTimeService : IPlayerTimeService
	{
		private readonly Dictionary<string, TimeSpan> _playerTimes = new();

		public TimeSpan GetPlayerTime(string playerName)
		{
			if (!_playerTimes.ContainsKey(playerName))
				_playerTimes.Add(playerName, TimeSpan.FromSeconds(0));

			return _playerTimes[playerName];
		}

		public TimeSpan RaisePlayerTime(string playerName)
		{
			if (_playerTimes.ContainsKey(playerName))
				_playerTimes[playerName] += TimeSpan.FromSeconds(1);

			return GetPlayerTime(playerName);
		}
	}
}