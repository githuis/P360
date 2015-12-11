using System;
using Ordersystem.Functions;
using System.Timers;

namespace Ordersystem
{
	public static class InactivityHandler
	{
		private static Timer inactivityTimer;

		public static void Start(LocalManager manager)
		{
			inactivityTimer = new Timer ();
			inactivityTimer.Interval = new TimeSpan(0, 5, 0).TotalMilliseconds; // 5 minutes of inactivity
			inactivityTimer.Elapsed += (sender, e) => {
				// manager.Logout();
			};
		}

		public static void Stop()
		{
			if (inactivityTimer == null)
				return;

			inactivityTimer.Stop ();
		}
	}
}

