using System.Threading.Tasks;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in IEvent>
        where IEvent : Event
    {
        Task Handle(IEvent @event);
    }

    public interface IEventHandler 
    {

    }

}