using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.UpdateOrderCommand
{
    public class UpdateOrderCommand : IRequest<Unit>
    {
        public int Codigo { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public int Ativo { get; set; }
    }
}
