using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flavio.SSO.Core.Application.ViewModels
{
	public class UserViewModel
	{
		[Required()]
		public Guid Id { get; set; }

		[Required()]
		public string Username { get; set; }

		[Required()]
		public string Name { get; set; }

		[Required()]
		public string Token { get; set; }

		[Required]
		public DateTime ExpirationTime { get; set; }

		[Required()]
		public DateTime LogonTime { get; set; }

		public ICollection<GroupViewModel> Groups;
	}
}
