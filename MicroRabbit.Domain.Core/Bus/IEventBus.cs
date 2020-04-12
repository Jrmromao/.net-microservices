using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using System.Threading.Tasks;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command; // restriction - > usinf the mediatr lib to send commands 

        void Publish<T>(T @event) where T : Event; // use the @ sign cz event is a reserved keyword


        // subscribe to the event
        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;

    }
}