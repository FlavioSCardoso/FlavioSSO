using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flavio.SSO.Core.UI.Web.Login.Models
{
	public class Configuration
	{
		public static string CookieNameStandard { get; set; } = "SSO.User.Token";
	}
}