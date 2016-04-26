using System.Web.Mvc;

namespace Flavio.SSO.Core.UI.Web.Login
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
