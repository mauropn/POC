using Application.Features.Commands.CreateOrderCommand;
using Application.Features.Commands.UpdateOrderCommand;
using Application.Features.Queries.GetAllOrders;
using Application.Features.Queries.GetOrderByCodigo;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<UpdateOrderCommand, Order>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<OrderByCodigoDTO, Order>().ReverseMap();
        }
    }
}
