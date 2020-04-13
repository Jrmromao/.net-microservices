using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Application.Servives
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _bus;


        public AccountService(IAccountRepository accountRepository, IEventBus bus)
        {
            _bus = bus;
            _accountRepository = accountRepository;
        }

        public IEnumerable<Account> GetAccount() => _accountRepository.GetAccount();

        public void Transfer(AccountTransfer accountTransfer)
        {
            // need to start making use of eventBus
            //throw new NotImplementedException();

            var createTransferCommand = new CreateTransferCommand(
               accountTransfer.FromAccount,
               accountTransfer.ToAccount,
               accountTransfer.TransferAmount

                );

            _bus.SendCommand(createTransferCommand);
        }
    }
}
