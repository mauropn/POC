using Application.Contracts.Logging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }
        public void LogInformation(string message, params object[] args)
        {
            //Envio para um log simples
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogToBroker(string message)
        {
            //Onde o meu Rabbit Brocker está rodando? 
            var factory = new ConnectionFactory { HostName = "localhost" };

            //Crio a connection... 
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //Criando os bindings exchanges...
            channel.ExchangeDeclare("logExchange", "direct", true, false, null);
            channel.ExchangeDeclare("exchangeSecundario", "direct", true, false, null);

            //Queues
            channel.QueueDeclare("queue01", true, false, false, null);
            channel.QueueDeclare("queue02", true, false, false, null);

            //Binding as duas queues aos exchanges...
            channel.QueueBind("queue01", "logExchange", "keyLog");
            channel.QueueBind("queue02", "exchangeSecundario", "key02");



            //Enviando uma mensagem para testar
            channel.BasicPublish(
                "logExchange",
                "keyLog",
                null,
                Encoding.UTF8.GetBytes(message));
        }
    }
}
