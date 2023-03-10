using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IOptions<DBsetting> dbsetting) { 


            //var client = new MongoClient(configuration.GetValue<string>("ConnectionStrings:ConnectionString"));
            //var databse = client.GetDatabase(configuration.GetValue<string>("ConnectionStrings: DatabaseName"));

            //Products = databse.GetCollection<Product>(configuration.GetValue<string>("ConnectionStrings: CollectionName"));
            var mongoclient = new MongoClient(dbsetting.Value.ConnectionString);
            var mongodatabase = mongoclient.GetDatabase(dbsetting.Value.DatabaseName);
            Products = mongodatabase.GetCollection<Product>(dbsetting.Value.CollectionName);

            CatalogContextSeed.SeedData(Products);

        }
         public IMongoCollection<Product> Products { get; }
    }
}
 