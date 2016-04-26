using System.ComponentModel.DataAnnotations;

namespace Flavio.SSO.Core.UI.Web.Login.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Please inform your user name"), Display(Name = "User Name")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Please inform your password")]
		public string Password { get; set; }
		public bool InvalidLogon { get; set; } 
	}
}