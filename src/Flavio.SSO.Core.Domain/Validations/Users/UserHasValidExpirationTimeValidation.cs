using DomainValidation.Validation;
using Flavio.SSO.Core.Domain.Entities;

namespace Flavio.SSO.Core.Domain.Validations.Users
{
	public class UserHasValidExpirationTimeValidation : Validator<User>
	{
		public UserHasValidExpirationTimeValidation()
		{
			var expiration = new Specifications.Users.UserHasValidExpirationTimeSpecification();

			base.Add("expiration", new Rule<User>(expiration, "User expired"));
		}
	}
}
