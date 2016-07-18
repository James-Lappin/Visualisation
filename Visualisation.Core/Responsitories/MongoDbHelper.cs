using MongoDB.Driver;
using System;
using System.Configuration;

namespace Visualisation.Core.Responsitories
{
	public class MongoDbHelper
	{
		public static IMongoDatabase GetDatabase()
		{
			var credential = MongoCredential.CreateCredential(GetDatabaseName(), GetUsername(), GetPassword());
			var mongoClientSettings = new MongoClientSettings
			{
				Credentials = new[] { credential },
				Server = new MongoServerAddress(GetDatabaseAddress(), GetDatabasePort()),
			};

			var client = new MongoClient(mongoClientSettings);
			return client.GetDatabase(GetDatabaseName());
		}

		public static IMongoCollection<T> GetCollection<T>()
		{
			return GetCollection<T>(GetDatabase());
		}

		public static IMongoCollection<T> GetCollection<T>(IMongoDatabase database)
		{
			return database.GetCollection<T>(typeof(T).Name);
		}

		private static string GetDatabaseAddress()
		{
			return ConfigurationManager
				.AppSettings
				.Get("MongoDbAddress");
		}

		private static int GetDatabasePort()
		{
			return Convert.ToInt32(ConfigurationManager
									.AppSettings
									.Get("MongoDbPort"));
		}

		private static string GetDatabaseName()
		{
			return ConfigurationManager
				.AppSettings
				.Get("MongoDbName");
		}

		private static string GetUsername()
		{
			return ConfigurationManager
				.AppSettings
				.Get("MongoDbUsername");
		}
		private static string GetPassword()
		{
			return ConfigurationManager
				.AppSettings
				.Get("MongoDbPassword");
		}
	}
}