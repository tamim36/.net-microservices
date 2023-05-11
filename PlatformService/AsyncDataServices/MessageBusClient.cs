using PlatformService.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine($"--> RabbitMQ connected successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> RabbitMQ connection error: {ex.Message}");
                throw;
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes( message );
            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"--> We have sent {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessgeBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("--> Connection open. Sending message ...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> Connection Closed not sending!");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("--> RabbitMq Connection Shutdown");
        }
    }
}
