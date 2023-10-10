using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Commands.CreateOrderCommand;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetAllOrdersFromQueue
{
    public class GetOrderFromQueueHandler : IRequestHandler<GetOrdersFromQueueQuery, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IModel _channel;
        private const string Queue = "orders-service";
        public GetOrderFromQueueHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

      
        public async Task<Unit> Handle(GetOrdersFromQueueQuery request, CancellationToken cancellationToken)
        {
            List<OrderDTO> orders = new List<OrderDTO>();

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "orderQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);


                    var orderList = JsonSerializer.Deserialize<List<Order>>(message);

                    //Salvo no banco.. 

                    foreach (var order in orderList)
                    {
                        _orderRepository.CreateAsync(order);
                    }

                }
                catch (Exception)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }

            };
            channel.BasicConsume(queue: "orderQueue",
                                 autoAck: false,
                                 consumer: consumer);

            return Unit.Value;
        }
    }
}
