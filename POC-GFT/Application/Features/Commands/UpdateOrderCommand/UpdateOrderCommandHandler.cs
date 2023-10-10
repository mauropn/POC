using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using AutoMapper;
using Domain;
using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.Commands.UpdateOrderCommand
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IAppLogger<UpdateOrderCommand> _logger;
        public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, IAppLogger<UpdateOrderCommand> appLogger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = appLogger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = _mapper.Map<Order>(request);

            var order = await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.LogToBroker($"Alterado com sucesso {JsonSerializer.Serialize(orderToUpdate)}");
           
            return Unit.Value;
        }
    }
}
