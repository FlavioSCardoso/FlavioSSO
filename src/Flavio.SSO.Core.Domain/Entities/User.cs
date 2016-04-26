using Flavio.SSO.Core.Domain.Validations.Users;
using DomainValidation.Validation;
using System;
using System.Collections.Generic;

namespace Flavio.SSO.Core.Domain.Entities
{
	public class User
	{
		public User()
		{
			Groups = new List<Group>();
		}
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public string Token { get; set; }
		public DateTime LogonTime { get; set; }
		public DateTime ExpirationTime { get; set; }
		public ICollection<Group> Groups { get; set; }
	}
}
