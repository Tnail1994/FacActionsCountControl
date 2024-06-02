using FacActionsCountControl.Control.Models;

namespace FacActionsCountControl.Services
{
	internal interface IPlayer
	{
		string Name { get; set; }
	}

	internal class Player : IPlayer
	{
		public static Player DefaultPlayer1 => new Player
		{
			Name = "Player 1",
		};

		public static Player DefaultPlayer2 => new Player
		{
			Name = "Player 2",
		};

		public string Name { get; set; }
	}
}