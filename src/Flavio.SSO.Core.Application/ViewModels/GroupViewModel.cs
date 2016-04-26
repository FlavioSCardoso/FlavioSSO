using System;
using System.ComponentModel.DataAnnotations;

namespace Flavio.SSO.Core.Application.ViewModels
{
	public class GroupViewModel
	{
		[Required()]
		public Guid Id { get; set; }

		[Required()]
		public string Name { get; set; }
	}
}