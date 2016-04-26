using System;

namespace Flavio.SSO.Core.Application
{
	internal class Configuration
	{
		private static int DefaultSlidingMinutes = 30;
		public static int SlidingMinutes
		{
			get
			{
				var minutes = System.Configuration.ConfigurationManager.AppSettings["SessionSlidingMinutes"];
				if (minutes == null)
				{
					return DefaultSlidingMinutes;
				}
				else
				{
					return Convert.ToInt32(minutes);
				}
			}
		}
	}
}