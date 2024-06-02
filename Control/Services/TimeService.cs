namespace FacActionsCountControl.Control.Services
{
	internal interface ITimeService
	{
		TimeSpan GetTime();
		TimeSpan RaisePlayerTime();
	}

	internal class TimeService : ITimeService
	{
		private TimeSpan _currentTime = TimeSpan.FromSeconds(0);
		public TimeSpan GetTime() => _currentTime;

		public TimeSpan RaisePlayerTime()
		{
			_currentTime += TimeSpan.FromSeconds(1);
			return _currentTime;
		}
	}
}