using Flavio.SSO.Core.Domain.Entities;
using Flavio.SSO.Core.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using Flavio.SSO.Core.Infra.Data.Context;

namespace Flavio.SSO.Core.Infra.Data.Repositories
{
	public class GroupRepository : Repository<Group>, IGroupRepository
	{
		public GroupRepository(CoreContext context)
            :base(context)
        {

		}

		public IEnumerable<Group> AddRange(ICollection<Group> groups)
		{
			return Db.Groups.AddRange(groups);
		}

		public void RemoveAllFromUser(Guid userId)
		{
			var user = Db.Users.Find(userId);
			if(user != null && user.Groups.Count > 0) {
				dbSet.RemoveRange(user.Groups);
			}
		}

		public IEnumerable<Group> GetGroupsById(IEnumerable<Guid> gids)
		{
			return Db.Groups.Where(g => gids.Contains(g.Id));
		}

	}
}
