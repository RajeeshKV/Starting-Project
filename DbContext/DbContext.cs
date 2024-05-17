using Microsoft.Azure.Cosmos;

namespace Starting_Project.DbContext
{
    public class DbContext
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Database _database;

        public DbContext(IConfiguration configuration)
        {
            _cosmosClient = new CosmosClient(configuration["CosmosDb:Account"], configuration["CosmosDb:Key"]);
            _database = _cosmosClient.CreateDatabaseIfNotExistsAsync(configuration["CosmosDb:DatabaseName"]).Result;
        }

        public async Task<Container> GetContainerAsync<T>() where T : class
        {
            var containerName = typeof(T).Name;
            var container = await _database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return container;
        }
    }
}
