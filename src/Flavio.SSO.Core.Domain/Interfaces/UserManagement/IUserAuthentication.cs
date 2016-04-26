using Flavio.SSO.Core.Domain.Entities;

namespace Flavio.SSO.Core.Domain.Interfaces.UserManagement
{
	public interface IUserAuthentication
	{
		User GetAuthenticatedUser(string userName, string password);
	}
}