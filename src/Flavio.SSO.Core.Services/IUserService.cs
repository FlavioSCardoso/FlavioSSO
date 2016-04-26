using Flavio.SSO.Core.Application.ViewModels;
using System;
using System.ServiceModel;

namespace Flavio.SSO.Core.Services
{
	[ServiceContract]
	public interface IUserService
	{
		[OperationContract]
		bool AuthenticatedUserIsValid(string token);

		[OperationContract]
		UserViewModel AuthenticateUser(string userName, string password);

		[OperationContract]
		UserViewModel GetAuthenticateUserByToken(string token);

		[OperationContract]
		void LogOffUser(string token);
	}
}
