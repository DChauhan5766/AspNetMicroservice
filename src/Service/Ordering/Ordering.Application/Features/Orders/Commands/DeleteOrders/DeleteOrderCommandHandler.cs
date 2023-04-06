using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrders
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommands>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommands> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommands> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteOrderCommands request , CancellationToken cancellationToken)
        {
            var orderdetails = await _orderRepository.GetByIdAsync(request.Id);
            if (orderdetails == null)
            {
                _logger.LogError("Order deleted successfully.");
            }
            await _orderRepository.DeleteAsync(orderdetails);

            _logger.LogInformation($"Order {orderdetails.Id} is successfully Deleted.");

            //return Unit.Value;
        }
    }
}
