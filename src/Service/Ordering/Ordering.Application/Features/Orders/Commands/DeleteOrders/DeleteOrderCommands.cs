using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrders
{
    public class DeleteOrderCommands : IRequest 
    {
        public int Id { get; set; }
    }
}
