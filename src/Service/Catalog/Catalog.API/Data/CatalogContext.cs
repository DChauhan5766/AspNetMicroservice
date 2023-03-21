using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration) {


            var client = new MongoClient(configuration.GetValue<string>("CatalogDatabase:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("CatalogDatabase:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("CatalogDatabase:CollectionName"));
            CatalogContextSeed.SeedData(Products);

        }
         public IMongoCollection<Product> Products { get; }
    }
}
 