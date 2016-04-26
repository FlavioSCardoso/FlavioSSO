using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flavio.SSO.Core.UI.Web.Login.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
			var cookieName = System.Configuration.ConfigurationManager.AppSettings["SSO-CookieName"] ?? Models.Configuration.CookieNameStandard;
			var cookie = new HttpCookie(cookieName);
			if(cookie != null)
			{
				using (SSOServices.UserServiceClient cliente = new SSOServices.UserServiceClient())
				{
					cliente.LogOffUser(cookie.Value);
				}
				cookie.Value = null;
				cookie.Expires = DateTime.Now.AddDays(-3);
				Response.SetCookie(cookie);
			}

			return View();
        }
    }
}