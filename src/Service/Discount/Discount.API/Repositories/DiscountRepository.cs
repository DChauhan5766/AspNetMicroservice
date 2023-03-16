using Discount.API.Entities;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<Coupon> GetDiscount(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
