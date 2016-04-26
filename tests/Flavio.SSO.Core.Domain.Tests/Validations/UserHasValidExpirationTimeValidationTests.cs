using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flavio.SSO.Core.Domain.Validations.Users;
using Flavio.SSO.Core.Domain.Entities;

namespace Flavio.SSO.Core.Domain.Tests.Validations
{
	[TestClass]
	public class UserHasValidExpirationTimeValidationTests
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

			UserHasValidExpirationTimeValidation validation = new UserHasValidExpirationTimeValidation();
			Assert.IsTrue(validation.Validate(usr).IsValid);

			usr.ExpirationTime = DateTime.Now.AddMinutes(-10);
			Assert.IsFalse(validation.Validate(usr).IsValid);

		}
	}
}
