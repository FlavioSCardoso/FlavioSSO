using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Flavio.SSO.Core.Domain.Interfaces.Repositories
{
	public interface IRepository<TEntity> : IDisposable where TEntity : class
	{
		TEntity SearchById(Guid id);
		void Remove(Guid id);
		IEnumerable<TEntity> SelectAll();
		TEntity Add(TEntity obj);
		TEntity Update(TEntity obj);
		IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
	}
}
