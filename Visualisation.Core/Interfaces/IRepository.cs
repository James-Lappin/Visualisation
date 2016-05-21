using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Visualisation.Core.Responsitories;

namespace Visualisation.Core.Interfaces
{
	public interface IRepository<TEntity> where TEntity : EntityBase
	{
		void CreateOrUpdate(TEntity entity);
		void Delete(TEntity entity);

		TEntity GetById(Guid id);
		IList<TEntity> GetAll();
		IList<TEntity> GetByFilter(Expression<Func<TEntity, bool>> predicate);
	}
}
