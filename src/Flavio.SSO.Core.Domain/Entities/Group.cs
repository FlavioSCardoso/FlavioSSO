using System;
using System.Collections.Generic;

namespace Flavio.SSO.Core.Domain.Entities
{
	public class Group
	{
		public Group()
		{
			Users = new List<User>();
		}
		public Guid Id { get; set; }
		public string Name { get; set; }

		public ICollection<User> Users { get; set; }
	}
}