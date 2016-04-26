using Flavio.SSO.Client.SecurityService;
using System;

namespace Flavio.SSO.Client
{
	class CachedItem
	{
		public string Token { get; set; }
		public UserViewModel User { get; set; }
		public DateTime Time { get; set; }
	}
}
