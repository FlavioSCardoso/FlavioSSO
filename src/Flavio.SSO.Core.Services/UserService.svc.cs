using Flavio.SSO.Core.Application.ViewModels;
using Flavio.SSO.Core.Application.Interfaces;

namespace Flavio.SSO.Core.Services
{
	public class UserService : IUserService
	{

		private readonly IUserManager _userManager;

		public UserService(IUserManager userManager)
		{
			_userManager = userManager;
		}

		public bool AuthenticatedUserIsValid(string token)
		{
			return _userManager.AuthenticatedUserIsValid(token);
		}

		public UserViewModel AuthenticateUser(string userName, string password)
		{
			return _userManager.AuthenticateUser(userName, password);
		}

		public UserViewModel GetAuthenticateUserByToken(string token)
		{
			return _userManager.GetAuthenticateUserByToken(token);
		}

		public void LogOffUser(string token)
		{
			_userManager.LogOffUser(token);
		}
	}
}
