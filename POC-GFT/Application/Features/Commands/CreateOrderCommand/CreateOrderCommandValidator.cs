using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Commands.CreateOrderCommand
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private IOrderRepository _orderRepository;

        public CreateOrderCommandValidator(IOrderRepository orderRepository)
        {
            RuleFor(o => o.OrderName)
                .NotEmpty().WithMessage("{OrderName} é obrigatório")
                .NotNull();

            this._orderRepository = orderRepository;
        }
    }
}