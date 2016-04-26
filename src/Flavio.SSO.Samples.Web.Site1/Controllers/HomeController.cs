using System.Web.Mvc;

namespace Flavio.SSO.Samples.Web.Site1.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{

			ViewBag.UserName = Request.ServerVariables["HTTP_SSO.USER"];
			ViewBag.UserGroups = Request.ServerVariables["HTTP_SSO.GROUPS"];
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}