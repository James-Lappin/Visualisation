using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Visualisation.Core.Interfaces;

namespace Visualisation.Core.Responsitories
{
	public class MongoDbRepository<TEntity> : IRepository<TEntity>
			where TEntity : IEntity
	{
		protected readonly IMongoCollection<TEntity> Collection;

		public MongoDbRepository()
		{
			Collection = MongoDbHelper.GetCollection<TEntity>();
		}

		public void CreateOrUpdate(TEntity entity)
		{
			if (entity.EntityId == Guid.Empty)
			{
				entity.EntityId = Guid.NewGuid();
				Collection.InsertOne(entity);
				return;
			}

			Collection.ReplaceOne(x => x.EntityId.Equals(entity.EntityId), entity);
		}

		public void Delete(TEntity entity)
		{
			Collection.DeleteOne(x => x.EntityId == entity.EntityId);
		}

		public void DeleteAll()
		{
			Collection.DeleteMany(FilterDefinition<TEntity>.Empty);
		}

		public TEntity GetById(Guid id)
		{
			return Collection.Find(x => x.EntityId.Equals(id))
							.FirstOrDefault();
		}

		public TEntity GetSingleByFilter(Expression<Func<TEntity, bool>> predicate)
		{
			return Collection
				.Find(predicate)
				.SingleOrDefault();
		}

		public IList<TEntity> GetAll()
		{
			return Collection.Find(FilterDefinition<TEntity>.Empty).ToList();
		}

		public IList<TEntity> GetByFilter(Expression<Func<TEntity, bool>> predicate)
		{
			return Collection
				.Find(predicate)
				.ToList();
		}
	}
}