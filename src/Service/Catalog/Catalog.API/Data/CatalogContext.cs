using Catalog.API.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration) { 

            var client = new MongoClient(configuration.GetValue<string>("ConnectionStrings:ConnectionString"));
            var databse = client.GetDatabase(configuration.GetValue<string>("ConnectionStrings: DatabaseName"));

            Products = databse.GetCollection<Product>(configuration.GetValue<string>("ConnectionStrings: CollectionName"));
            CatalogContextSeed.SeedData(Products);

        }
         public IMongoCollection<Product> Products { get; }
    }
}
 