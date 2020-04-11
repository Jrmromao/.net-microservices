using MediatR;
using MicroRabbit.Domain.Core.Bus;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.infra.Bus
{
    public sealed class RabbitMQBus : IEventBus

    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;


        public RabbitMQBus(IMediator mediator)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }
        public Task SendCommand<T>(T command) where T : Command
        => _mediator.Send(command);
        

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost   "};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {

                var eventName = @event.GetType().Name;
                channel.QueueDeclare(eventName, false, false, false, null);
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var v2 = Encoding.UTF8.GetString(body);
                channel.BasicPublish("", eventName, null, body);
            }
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {

            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);


            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            if (_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Argument Type {handlerType.Name} is already registered for '{eventName}'", nameof(handlerType));

            }
            _handlers[eventName].Add(handlerType);
                StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {

            var factory = new ConnectionFactory() {
            HostName = "localhost",
            DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var eventName = typeof(T).Name; // reflection 

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new RabbitMQ.Client.Events.AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;
            channel.BasicConsume(eventName, true, consumer);
        }
        private async Task Consumer_Received(object sender, RabbitMQ.Client.Events.BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body);

            // kick the handler

            try
            {

                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }catch(Exception ex)
            {

            }
        
        }

        private  async Task ProcessEvent(string eventName, string message)
        {
            // look throu the dict of handles and check it contains the key
            if (_handlers.ContainsKey(eventName))
            {
                var subscriptions = _handlers[eventName];

                foreach (var s in subscriptions)
                {
                    var handler = Activator.CreateInstance(s);
                    if (handler == null) continue;
                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }


            }



        }
    }
}
