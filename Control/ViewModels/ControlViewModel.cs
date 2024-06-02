using CommunityToolkit.Mvvm.ComponentModel;
using FacActionsCountControl.Control.Models;

namespace FacActionsCountControl.Control.ViewModels
{
	internal interface IControlViewModel
	{
		IActionsCountModel ActionsCount { get; }
		IOverviewModel Overview { get; }
	}

	internal partial class ControlViewModel : ObservableObject, IControlViewModel
	{
		public IActionsCountModel ActionsCount { get; } = ActionsCountModel.Default;
		public IOverviewModel Overview { get; } = OverviewModel.Default;
	}
}