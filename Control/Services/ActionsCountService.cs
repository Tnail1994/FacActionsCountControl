namespace FacActionsCountControl.Control.Services
{
	internal interface IActionsCountService
	{
		void Reset();
		ActionsCountType GetNextAction();
		ActionsCountType GetCurrentAction();
	}

	internal class ActionsCountService : IActionsCountService
	{
		private ActionsCountType _nextRaisingActionsCountType;

		private List<ActionsCountType> _unRaisedActionsCountTypes;

		private List<ActionsCountType> GetActionsCountTypes() => new List<ActionsCountType>
		{
			ActionsCountType.Draw,
			ActionsCountType.Summon,
			ActionsCountType.Spell,
			ActionsCountType.Buy,
			ActionsCountType.Attack,
			ActionsCountType.Attach,
		};


		public ActionsCountService()
		{
			_unRaisedActionsCountTypes = GetActionsCountTypes();
		}

		public void Reset()
		{
			_unRaisedActionsCountTypes = GetActionsCountTypes();
		}

		public ActionsCountType GetNextAction()
		{
			if (_unRaisedActionsCountTypes.Count == 0)
			{
				Reset();
			}

			int randomIndex = new Random().Next(0, _unRaisedActionsCountTypes.Count);
			_nextRaisingActionsCountType = _unRaisedActionsCountTypes[randomIndex];
			_unRaisedActionsCountTypes.RemoveAt(randomIndex);
			return _nextRaisingActionsCountType;
		}

		public ActionsCountType GetCurrentAction()
		{
			return _nextRaisingActionsCountType;
		}
	}
}