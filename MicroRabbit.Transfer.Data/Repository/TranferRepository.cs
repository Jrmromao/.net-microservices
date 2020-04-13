using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using System.Collections.Generic;

namespace MicroRabbit.Transfer.Data.Repository
{
    public class TranferRepository : ITransferRepository
    {

        private TransferDbContext _ctx;

        public TranferRepository(TransferDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(TranferLog tranferLog){ 
            _ctx.Add(tranferLog); 
            _ctx.SaveChanges();
        }

        public IEnumerable<TranferLog> GetTranferLogs() => _ctx.TransferLogs;


    }
}
