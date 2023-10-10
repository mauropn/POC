using MediatR;


namespace Application.Features.Queries.GetOrderByCodigo
{
    public record GetOrderByCodigoQueueQuery(int Codigo) : IRequest<OrderByCodigoDTO>;
}
