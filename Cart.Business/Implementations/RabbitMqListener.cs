using Cart.Business.Configuration;
using Cart.Business.Interfaces;
using Cart.Business.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Cart.Business.Implementations
{
    public class RabbitMqListener : BackgroundService
    {
        private readonly AppSettings _appSettings;
        private readonly ICartingService _cartService;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqListener(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = serviceScopeFactory.CreateScope();

            _cartService = scope.ServiceProvider.GetRequiredService<ICartingService>();
            _appSettings = scope.ServiceProvider.GetRequiredService<AppSettings>();

            var factory = new ConnectionFactory { HostName = _appSettings.RabbitMqServerSettings.ConnectionString };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _appSettings.RabbitMqServerSettings.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var product = JsonConvert.DeserializeObject<ProductMessage>(content);

                _cartService.UpdateItems(new[] { product });
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_appSettings.RabbitMqServerSettings.Queue, false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();

            base.Dispose();
        }
    }
}
