using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetAllOrders
{
    public record GetOrderQuery : IRequest<List<OrderDTO>>;
}
