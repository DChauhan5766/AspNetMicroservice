using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeeds
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeeds> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Deepak", LastName = "chauhan", EmailAddress = "dchauhan2219@gmail.com", AddressLine = "Noida, Sector 168", Country = "India", TotalPrice = 350 }
            };
        }
    }
}
