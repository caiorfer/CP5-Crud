using MongoDB.Driver;
using Data.Settings;
using Models;

namespace Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbSettings _mongoDbSettings;
        public MongoDbContext(MongoDbSettings mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);

            _database = client.GetDatabase(mongoDbSettings.DatabaseName);

            _mongoDbSettings = mongoDbSettings;
        }
            public IMongoCollection<MovieModel> Filmes => _database.GetCollection<MovieModel>(_mongoDbSettings.CollectionName);

    }
}
