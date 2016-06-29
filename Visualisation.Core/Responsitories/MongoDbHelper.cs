using MongoDB.Driver;
using System.Configuration;

namespace Visualisation.Core.Responsitories
{
    public class MongoDbHelper
    {
        public static IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
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

        private static string GetConnectionString()
        {
            return ConfigurationManager
                .AppSettings
                .Get("MongoDbConnectionString")
                .Replace("{DB_NAME}", GetDatabaseName());
        }

        private static string GetDatabaseName()
        {
            return ConfigurationManager
                .AppSettings
                .Get("MongoDbDatabaseName");
        }

    }
}