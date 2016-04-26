using System.Web.Mvc;
using Flavio.SSO.Core.UI.Web.Login.Models.ViewModels;
using Flavio.SSO.Core.UI.Web.Login.SSOServices;
using System.Web;
using System;

namespace Flavio.SSO.Core.UI.Web.Login.Controllers
{
	public class HomeController : Controller
	{

		public ActionResult Index(string returnUrl)
		{
			if (string.IsNullOrEmpty(returnUrl))
				return RedirectToAction("NoReturnUrl");

			var cookieName = System.Configuration.ConfigurationManager.AppSettings["SSO-CookieName"] ?? Models.Configuration.CookieNameStandard;
			if (Request.Cookies[cookieName] != null)
			{
				var cookie = Request.Cookies[cookieName];
				cookie.Expires = DateTime.Now.AddDays(-1);
				Response.SetCookie(cookie);
			}

			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				model.InvalidLogon = true;
				return View(model);
			}

			UserViewModel authenticatedUser = null;
			using (SSOServices.UserServiceClient cliente = new SSOServices.UserServiceClient())
			{
				authenticatedUser = cliente.AuthenticateUser(model.UserName, model.Password);
			}
			if (authenticatedUser == null)
			{
				ViewBag.ReturnUrl = returnUrl;
				model.InvalidLogon = true;
				return View(model);
			}
			else
			{
				var cookieName = System.Configuration.ConfigurationManager.AppSettings["SSO-CookieName"] ?? Models.Configuration.CookieNameStandard;
				var cookie = new HttpCookie(cookieName);
				cookie.Value = authenticatedUser.Token;
				cookie.Expires = authenticatedUser.ExpirationTime;
				Response.SetCookie(cookie);
				return new RedirectResult(returnUrl);
			}
		}


		public ActionResult About()
		{
			return View();
		}

		public ActionResult NoReturnUrl()
		{
			return View();
		}
	}
}