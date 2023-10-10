using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.CreateOrderCommand
{
    public class CreateOrderCommand:IRequest<int>
    {
        public string OrderName { get; set; } = string.Empty;
        public int Ativo { get; set; }
    }
}
