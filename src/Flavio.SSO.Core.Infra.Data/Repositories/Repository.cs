using Flavio.SSO.Core.Infra.Data.Context;
using Flavio.SSO.Core.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Flavio.SSO.Core.Infra.Data.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected CoreContext Db;
		protected DbSet<TEntity> dbSet;

		public Repository(CoreContext context)
		{
			Db = context;
			dbSet = Db.Set<TEntity>();
		}

		public TEntity Add(TEntity obj)
		{
			var newOne = dbSet.Add(obj);
			return newOne;
		}

		public void Remove(Guid id)
		{
			dbSet.Remove(dbSet.Find(id));
		}

		public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
		{
			return dbSet.Where(predicate);
		}

		public TEntity SearchById(Guid id)
		{
			return dbSet.Find(id);
		}

		public IEnumerable<TEntity> SelectAll()
		{
			return dbSet.ToList();
		}

		public TEntity Update(TEntity obj)
		{
			var updated = Db.Entry(obj);
			dbSet.Attach(obj);
			updated.State = EntityState.Modified;

			return updated.Entity;
		}

		public void Dispose()
		{
			Db.Dispose();
			GC.SuppressFinalize(this);
		}

	}
}
