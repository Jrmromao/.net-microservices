using MicroRabbit.Domain.Core.Events;
using System.Threading.Tasks;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in IEvent> : IEventHandler where IEvent : Event{Task Handle(IEvent @event);}
    public interface IEventHandler {}

}