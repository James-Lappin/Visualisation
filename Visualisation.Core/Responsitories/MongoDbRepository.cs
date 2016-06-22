using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using Visualisation.Core.Interfaces;

namespace Visualisation.Core.Responsitories
{
	public class MongoDbRepository<TEntity> : IRepository<TEntity>
			where TEntity : IEntity
	{
		protected readonly IMongoCollection<TEntity> Collection;
		protected readonly IMongoDatabase Database;

		public MongoDbRepository()
		{
			Database = GetDatabase();
			Collection = GetCollection();
		}

		public void CreateOrUpdate(TEntity entity)
		{
			if (entity.Id == Guid.Empty)
			{
				entity.Id = Guid.NewGuid();
				Collection.InsertOne(entity);
				return;
			}

			Collection.ReplaceOne(x => x.Id.Equals(entity.Id), entity);
		}

		public void Delete(TEntity entity)
		{
			Collection.DeleteOne(x => x.Id == entity.Id);
		}

		public void DeleteAll()
		{
			Collection.DeleteMany(FilterDefinition<TEntity>.Empty);
		}

		public TEntity GetById(Guid id)
		{
			return Collection.Find(x => x.Id.Equals(id))
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

		#region Private Helper Methods

		private IMongoCollection<TEntity> GetCollection()
		{
			return Database.GetCollection<TEntity>(typeof(TEntity).Name);
		}

		private IMongoDatabase GetDatabase()
		{
			var client = new MongoClient(GetConnectionString());
			return client.GetDatabase(GetDatabaseName());
		}

		private string GetConnectionString()
		{
			return ConfigurationManager
				.AppSettings
				.Get("MongoDbConnectionString")
				.Replace("{DB_NAME}", GetDatabaseName());
		}

		private string GetDatabaseName()
		{
			return ConfigurationManager
				.AppSettings
				.Get("MongoDbDatabaseName");
		}

		#endregion
	}
}