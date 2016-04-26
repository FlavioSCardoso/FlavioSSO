using System;
using Flavio.SSO.Core.Application.ViewModels;

namespace Flavio.SSO.Core.Application.Interfaces
{
	public interface IUserManager
	{
		bool AuthenticatedUserIsValid(string token);
		UserViewModel AuthenticateUser(string userName, string password);
		UserViewModel GetAuthenticateUserByToken(string token);
		void Dispose();
		void LogOffUser(string token);
	}
}