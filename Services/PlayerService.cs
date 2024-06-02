namespace FacActionsCountControl.Services
{
	internal interface IPlayerService
	{
		string GetNextPlayerName(string currentPlayer);
		string GetFirstPlayerName();
	}

	internal class PlayerService : IPlayerService
	{
		private readonly IPlayer _player1, _player2;

		public PlayerService()
		{
			_player1 = Player.DefaultPlayer1;
			_player2 = Player.DefaultPlayer2;
		}

		public string GetNextPlayerName(string currentPlayer)
		{
			return currentPlayer switch
			{
				"Player 1" => _player2.Name,
				"Player 2" => _player1.Name,
				_ => throw new ArgumentOutOfRangeException(nameof(currentPlayer))
			};
		}

		public string GetFirstPlayerName()
		{
			return _player1.Name;
		}
	}
}