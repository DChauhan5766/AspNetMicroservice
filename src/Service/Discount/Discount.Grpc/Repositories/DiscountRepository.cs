﻿using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("insert into Coupon(ProductName, Description, Amount) values (@PdName, @Desc, @Amt)",
                new { PdName = coupon.ProductName, Desc = coupon.Description, Amt = coupon.Amount });


            if (affected==0)
                return false;

             return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("delete from Coupon where ProductName = @ProductName", new { ProductName = productName });


            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("select * from coupon where ProductName = @ProductName", new { ProductName = productName });
            if(coupon == null)
            {
                return new Coupon { ProductName= "No Discount", Amount = 0 , Description = "No Discount Desc"};
            }
            return coupon;
            
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
              (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("update Coupon set ProductName = @PdName, Description =@Desc, Amount=@Amt where Id = @id",
                new { PdName = coupon.ProductName, Desc = coupon.Description, Amt = coupon.Amount, id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
            
        }
    }
}
