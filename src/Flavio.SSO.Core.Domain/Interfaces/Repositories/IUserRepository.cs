using Flavio.SSO.Core.Domain.Entities;

namespace Flavio.SSO.Core.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<User>
	{ 
		User SearchByToken(string token);
	}
}
