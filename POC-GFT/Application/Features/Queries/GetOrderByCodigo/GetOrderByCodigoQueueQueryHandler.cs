using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetOrderByCodigo
{
    public class GetOrderByCodigoQueueQueryHandler : IRequestHandler<GetOrderByCodigoQueueQuery, OrderByCodigoDTO>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public GetOrderByCodigoQueueQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderByCodigoDTO> Handle(GetOrderByCodigoQueueQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Codigo);

            if(order == null)
            {
                throw new NotFoundException("Ordem", $"Codigo: {request.Codigo} não foi encontrado");
            }

            var data = _mapper.Map<OrderByCodigoDTO>(order);

            return data;
        }
    }
}
