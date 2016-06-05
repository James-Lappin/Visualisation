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
			where TEntity : EntityBase
	{
		private IMongoCollection<TEntity> _collection;
		private IMongoDatabase _database;

		public MongoDbRepository()
		{
			_database = GetDatabase();
			_collection = GetCollection();
		}

		public void CreateOrUpdate(TEntity entity)
		{
			if (entity.Id == Guid.Empty)
			{
				entity.Id = Guid.NewGuid();
				_collection.InsertOne(entity);
				return;
			}

			_collection.ReplaceOne(x => x.Id.Equals(entity.Id), entity);
		}

		public void Delete(TEntity entity)
		{
			_collection.DeleteOne(x => x.Id == entity.Id);
		}

		public void DeleteAll()
		{
			_collection.DeleteMany(FilterDefinition<TEntity>.Empty);
		}

		public TEntity GetById(Guid id)
		{
			return _collection.Find(x => x.Id.Equals(id))
							.FirstOrDefault();
		}

		public TEntity GetSingleByFilter(Expression<Func<TEntity, bool>> predicate)
		{
			return _collection
				.Find(predicate)
				.SingleOrDefault();
		}

		public IList<TEntity> GetAll()
		{
			return _collection.Find(FilterDefinition<TEntity>.Empty).ToList();
		}

		public IList<TEntity> GetByFilter(Expression<Func<TEntity, bool>> predicate)
		{
			return _collection
				.Find(predicate)
				.ToList();
		}

		#region Private Helper Methods

		private IMongoCollection<TEntity> GetCollection()
		{
			return _database.GetCollection<TEntity>(typeof(TEntity).Name);
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