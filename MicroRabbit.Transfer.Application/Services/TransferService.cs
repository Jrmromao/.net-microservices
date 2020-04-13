using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using System.Collections.Generic;

namespace MicroRabbit.Transfer.Application.Servives
{
    public class TransferService : ITransferService
    {

        private readonly ITransferRepository _tranferRepository;
        private readonly IEventBus _bus;


        public TransferService(ITransferRepository tranferRepository, IEventBus bus)
        {
            _bus = bus;
            _tranferRepository = tranferRepository;
        }

        public IEnumerable<TranferLog> GetTransferLogs()
        {
            return _tranferRepository.GetTranferLogs();
        }
    }
}
