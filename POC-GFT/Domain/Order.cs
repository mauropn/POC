using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order: BaseEntity
    {
        public string OrderName { get; set; } = string.Empty;
    }
}
