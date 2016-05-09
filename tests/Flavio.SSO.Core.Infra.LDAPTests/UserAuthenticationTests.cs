using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flavio.SSO.Core.Infra.LDAP;
using System;
using Flavio.SSO.Core.Domain.Interfaces.UserManagement;
using Flavio.SSO.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Flavio.SSO.Core.Infra.LDAPTests
{
	[TestClass]
	public class UserAuthenticationTests
	{


		//Integration Tests

		[TestMethod]
		public void ShouldGetUserWithUsernameAndPassword()
		{

			string user = System.Configuration.ConfigurationManager.AppSettings["DomainUserName"];
			string password = System.Configuration.ConfigurationManager.AppSettings["DomainUserPassword"];
			UserAuthentication userAuthentication = new UserAuthentication();
			var returning = userAuthentication.GetAuthenticatedUser(user, password);
			Assert.AreEqual(user, returning.Username);
		}

		[TestMethod]
		public void ShouldAuthenticateUserAndRetrieveGroups()
		{
			string user = System.Configuration.ConfigurationManager.AppSettings["DomainUserName"];
			string password = System.Configuration.ConfigurationManager.AppSettings["DomainUserPassword"];
			UserAuthentication userAuthentication = new UserAuthentication();
			var returning = userAuthentication.GetAuthenticatedUser(user, password);
			Assert.AreEqual(user, returning.Username);

			if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DomainUserGroupCount"]))
			{
				int userGroupsCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DomainUserGroupCount"]);
				Assert.AreEqual(userGroupsCount, returning.Groups.Count);
			}
		}
	}
}
