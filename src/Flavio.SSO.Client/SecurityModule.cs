using Flavio.SSO.Client.SecurityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Flavio.SSO.Client
{
	class SecurityModule : IHttpModule
	{
		private UserServiceClient _serviceClient;
		private string _loginUrl;
		private List<CachedItem> _recentUsers;
		private int _secondsInCache;
		private int _secondsInCacheStandard = 5;
		private string _HttpHeaderUserNameDescription;
		private string _HttpHeaderUserNameDescriptionStandard = "SSO.User";
		private string _HttpHeaderGroupsDescription;
		private string _HttpHeaderGroupsDescriptionStandard = "SSO.Groups";
		private string _SSOCookieName;
		private string _SSOCookieNameStandard = "SSO.User.Token";
		private string _groupsSeparator;
		private string _groupsSeparatorStandard = ",";
		private string _SSOServiceUrl;

		public void Init(HttpApplication application)
		{
			LoadConfiguration();

			_recentUsers = new List<CachedItem>();
			application.BeginRequest += new EventHandler(this.Context_BeginRequest);
		}

		private void InitializeService()
		{
			_serviceClient = ServiceConfigurator.CreateService(_SSOServiceUrl);
		}

		private void LoadConfiguration()
		{
			_SSOServiceUrl = System.Configuration.ConfigurationManager.AppSettings["SSO-ServiceUrl"];
			_loginUrl = System.Configuration.ConfigurationManager.AppSettings["SSO-LoginUrl"];

			string secondsInCache = System.Configuration.ConfigurationManager.AppSettings["SSO-SecondsInCache"];
			if (string.IsNullOrEmpty(secondsInCache) && !int.TryParse(secondsInCache, out _secondsInCache))
			{
				_secondsInCache = _secondsInCacheStandard;
			}
			_secondsInCache *= -1;

			_HttpHeaderUserNameDescription = System.Configuration.ConfigurationManager.AppSettings["SSO-HttpHeaderUserNameDescription"] ?? _HttpHeaderUserNameDescriptionStandard;
			_HttpHeaderGroupsDescription = System.Configuration.ConfigurationManager.AppSettings["SSO-HttpHeaderGroupsDescription"] ?? _HttpHeaderGroupsDescriptionStandard;
			_groupsSeparator = System.Configuration.ConfigurationManager.AppSettings["SSO-GroupsSeparator"] ?? _groupsSeparatorStandard;
			_SSOCookieName = System.Configuration.ConfigurationManager.AppSettings["SSO-CookieName"] ?? _SSOCookieNameStandard;

		}

		internal void Context_BeginRequest(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;
			HttpContext context = application.Context;

			if (!ConfigurationsAreValid(application.Context))
				return;

			if(_serviceClient ==null)
				InitializeService();

			var cookie = context.Request.Cookies[_SSOCookieName];
			if (cookie == null)
			{
				RedirectUserToLogin(context);
				return;
			}
			else
			{
				string name, groups;

				var validUser = GetValidUser(cookie.Value);
				if(validUser == null) {
					RedirectUserToLogin(context);
					return;
				}

				name = validUser.Name;
				groups = JoinUserGroups(validUser.Groups);
				context.Request.Headers.Add(_HttpHeaderUserNameDescription, name);
				context.Request.Headers.Add(_HttpHeaderGroupsDescription, groups);

				var newCookie = new HttpCookie(_SSOCookieName, validUser.Token);
				newCookie.Expires = validUser.ExpirationTime;
				context.Response.Cookies.Add(newCookie);
			}
		}

		private bool ConfigurationsAreValid(HttpContext context)
		{
			if(string.IsNullOrEmpty(_loginUrl) || string.IsNullOrEmpty(_SSOServiceUrl)) {
				context.Response.Write("<h1 style='color: red'>Please, the parameters <u><i>LoginUrl</i></u> and <u><i>SSOServiceUrl</i></u> are mandatory in application's Web.Config AppSettings section.</h1>");
				context.Response.Write("<h3>It wold be nice some documentation here!.</h3>");
				context.Response.End();
				return false;
			}
			return true;
		}

		internal UserViewModel GetValidUser(string token)
		{
			var recentUser = _recentUsers.Where(r => r.Token == token).FirstOrDefault();

			var expiredFromCache = false;
			if (recentUser != null)
				expiredFromCache = (recentUser.Time > DateTime.Now.AddSeconds(_secondsInCache));

			if (recentUser == null || expiredFromCache)
			{
				if (expiredFromCache)
					_recentUsers.Remove(recentUser);

				var newUser = GetUserFromSSO(token);
				if(newUser != null) {
					_recentUsers.Add(new CachedItem
					{
						Time = DateTime.Now,
						Token = token,
						User = newUser
					});
					return newUser;
				}
			}
			else
			{
				return recentUser.User;
			}
			return null;
		}

		internal UserViewModel GetUserFromSSO(string token)
		{
			var newValidUser = _serviceClient.GetAuthenticateUserByToken(token);
			if (newValidUser == null)
			{
				return null;
			}

			return newValidUser;
		}

		internal string JoinUserGroups(ICollection<GroupViewModel> groups)
		{
			return string.Join(_groupsSeparator, groups.Select(g => g.Name));
		}

		internal void RedirectUserToLogin(HttpContext context)
		{
			context.Response.Redirect(string.Format("{0}?returnUrl={1}", _loginUrl, context.Server.UrlEncode(context.Request.Url.ToString())));
		}

		public void Dispose()
		{
			_serviceClient.Close();
			_serviceClient = null;
			GC.Collect();
		}



	}
}
