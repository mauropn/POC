using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetAllOrdersFromQueue
{
    
        public class OrderDTO
        {
            public int Codigo { get; set; }
            public string OrderName { get; set; }
            public DateTime DataCadastro { get; set; }
            public int Ativo { get; set; }
        }
}
