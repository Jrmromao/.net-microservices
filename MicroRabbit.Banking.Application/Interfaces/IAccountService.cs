using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Application.Interfaces
{
   public interface IAccountService
    {
        IEnumerable<Account> GetAccount();
        // add a method to tranfer funds 
        void Transfer(AccountTransfer accountTransfer);

    }
}
