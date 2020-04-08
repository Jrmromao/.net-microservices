using MicroRabbit.Domain.Core.Events;
using System;

namespace MicroRabbit.Domain.Core.Bus
{
    public abstract class Command : Message
    {

        public DateTime TimeStamp { get; protected set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}