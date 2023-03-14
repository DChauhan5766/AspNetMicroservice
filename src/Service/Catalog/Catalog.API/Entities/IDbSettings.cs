namespace Catalog.API.Entities
{
    public interface IDbSettings
    {
         string ConnectionString { get; set; } 
         string DatabaseName { get; set; }
         string CollectionName { get; set; }
    }
}
