using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IMapper mapper ,IDiscountRepository discountRepository , ILogger<DiscountService> logger )
        {
            _repository = discountRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.Productname);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productname = {request.Productname} not found."));
            }
            _logger.LogInformation("Discount is retrived for ProductName : {productName}, Amount:{Amount}", coupon.ProductName, coupon.Amount);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.CreateDiscount(coupon);

            _logger.LogInformation("Discount successfully created. ProductName : {ProductName}", coupon.ProductName);
            var couponmodel = _mapper.Map<CouponModel>(coupon);

            return couponmodel;

        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.UpdateDiscount(coupon);

            _logger.LogInformation("Discount successfully updated. ProductName : {ProductName}", coupon.ProductName);

            var couponmodel = _mapper.Map<CouponModel>(coupon);
            return couponmodel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.Productname);
            var response = new DeleteDiscountResponse
            {
                Success = deleted,
            };
            return response;
        }
    }
}
