using Discount.Grpc.Protos;

namespace Basket.API.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _client;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
        {
            _client = client;
        }

        public async Task<CouponModel> GetDiscount( string ProductName)
        {
            var discountrequest =  new GetDiscountRequest { Productname= ProductName };
            return await _client.GetDiscountAsync(discountrequest);

        }
    }
}
