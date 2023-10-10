using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;


namespace Application.Features.Commands.CreateOrderCommand
{
    public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator(_orderRepository);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Any())
                throw new BadRequestException($"Order inválida.", validatorResult);

            //Converto para o objeto da entidade
            var orderToCreate = _mapper.Map<Order>(request);
            await _orderRepository.CreateAsync(orderToCreate);

            return orderToCreate.Codigo;
        }
    }
}
