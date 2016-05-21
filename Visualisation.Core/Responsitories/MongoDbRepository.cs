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
		internal IMongoDatabase Database;
		private IMongoCollection<TEntity> _collection;

		public MongoDbRepository()
		{
			GetDatabase();
			GetCollection();
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
		private void GetDatabase()
		{
			var client = new MongoClient(GetConnectionString());
			Database = client.GetDatabase(GetDatabaseName());
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

		private void GetCollection()
		{
			_collection = Database
				.GetCollection<TEntity>(typeof(TEntity).Name);
		}
		#endregion
	}
}