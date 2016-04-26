using Flavio.SSO.Core.Application.Interfaces;
using Flavio.SSO.Core.Infra.CrossCutting.IoC;
using Flavio.SSO.Core.ServicesTests.ServiceReference;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;
using SimpleInjector.Integration.Wcf;

namespace Flavio.SSO.Core.ServicesTests
{
	[TestClass]
	public class UserServiceTests
	{

		[TestMethod]
		public void VerifyServiceBehaviour()
		{
			string _userName;
			string _userPassword;
			int _userGroupCount;

			_userName = System.Configuration.ConfigurationManager.AppSettings["DomainUserName"];
			_userPassword = System.Configuration.ConfigurationManager.AppSettings["DomainUserPassword"];
			_userGroupCount = int.Parse(System.Configuration.ConfigurationManager.AppSettings["DomainUserGroupCount"]);

			SimpleInjector.Container container = new SimpleInjector.Container();
			BootStrapper.RegisterForNonWebApps(container);

			IUserManager userManager = container.GetInstance<IUserManager>();

			using (SimpleInjectorServiceHost serviceHost = new SimpleInjectorServiceHost(container, typeof(Services.UserService)))
			{
				var webHttpBinding = new BasicHttpBinding();
				var uri = new Uri("http://localhost:1234/UserService");
				serviceHost.AddServiceEndpoint(typeof(Services.IUserService), webHttpBinding, uri);

				serviceHost.Open();

				EndpointAddress remoteAddress = new EndpointAddress(uri);

				using (UserServiceClient client = new UserServiceClient(webHttpBinding, remoteAddress))
				{
					UserViewModel user = client.AuthenticateUser(_userName, _userPassword);
					Assert.IsNotNull(user);
					Assert.IsTrue(client.AuthenticatedUserIsValid(user.Token));
					client.LogOffUser(user.Token);
					Assert.IsFalse(client.AuthenticatedUserIsValid(user.Token));
				}

			}

		}
	}
}
