using Flavio.SSO.Core.Domain.Entities;
using Flavio.SSO.Core.Domain.Interfaces.Repositories;
using Flavio.SSO.Core.Infra.Data.Context;
using System.Linq;

namespace Flavio.SSO.Core.Infra.Data.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(CoreContext context)
			: base(context)
		{

		}

		public User SearchByToken(string token)
		{
			return dbSet.Include("Groups").FirstOrDefault(u => u.Token == token);
		}

	}
}
