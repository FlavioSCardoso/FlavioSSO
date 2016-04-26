using Flavio.SSO.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Flavio.SSO.Core.Domain.Interfaces.Repositories
{
	public interface IGroupRepository : IRepository<Group>
	{
		void RemoveAllFromUser(Guid userId);
		IEnumerable<Group> AddRange(ICollection<Group> groups);
		IEnumerable<Group> GetGroupsById(IEnumerable<Guid> gids);
	}

}
