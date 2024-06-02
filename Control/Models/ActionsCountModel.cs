using CommunityToolkit.Mvvm.ComponentModel;

namespace FacActionsCountControl.Control.Models
{
	internal interface IActionsCountModel
	{
		int Draw { get; set; }
		int Summon { get; set; }
		int Spell { get; set; }
		int Buy { get; set; }
		int Attach { get; set; }
		int Attack { get; set; }
	}

	internal partial class ActionsCountModel : ObservableObject, IActionsCountModel
	{
		[ObservableProperty] private int _draw, _summon, _spell, _buy, _attach, _attack;

		public static IActionsCountModel Default => new ActionsCountModel
		{
			Draw = 4,
			Summon = 1,
			Spell = 1,
			Buy = 1,
			Attach = 1,
			Attack = 1
		};
	}
}