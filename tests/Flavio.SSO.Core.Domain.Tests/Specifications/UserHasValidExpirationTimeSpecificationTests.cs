using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flavio.SSO.Core.Domain.Entities;
using Flavio.SSO.Core.Domain.Specifications.Users;

namespace Flavio.SSO.Core.Domain.Tests.Specifications
{
	[TestClass]
	public class UserHasValidExpirationTimeSpecificationTests
	{
		[TestMethod]
		public void UserHasExpiredDateTests()
		{
			Guid guid = Guid.NewGuid();
			User usr = new User()
			{
				ExpirationTime = DateTime.Now.AddMinutes(90),
				Id = guid,
				Name = "Tests",
				LogonTime = DateTime.Now,
				Username = "testUsername",
				Token = string.Empty
			};

			UserHasValidExpirationTimeSpecification specification = new UserHasValidExpirationTimeSpecification();
			Assert.IsTrue(specification.IsSatisfiedBy(usr));

			usr.ExpirationTime = DateTime.Now.AddMinutes(-10);
			Assert.IsFalse(specification.IsSatisfiedBy(usr));

		}
	}
}
