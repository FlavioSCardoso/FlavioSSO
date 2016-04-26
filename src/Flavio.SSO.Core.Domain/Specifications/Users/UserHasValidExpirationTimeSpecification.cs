using DomainValidation.Interfaces.Specification;
using Flavio.SSO.Core.Domain.Entities;
using System;

namespace Flavio.SSO.Core.Domain.Specifications.Users
{
	public class UserHasValidExpirationTimeSpecification : ISpecification<User>
	{
		public bool IsSatisfiedBy(User entity)
		{
			return entity.ExpirationTime > DateTime.Now;
		}

	}
}
