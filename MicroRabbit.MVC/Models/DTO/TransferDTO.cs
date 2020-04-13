using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRabbit.MVC.Models.DTO
{

    // whats req for the banking controler
    public class TransferDTO
    {
        public int ToAccount { get; set; }
        public int FromAccount { get; set; }
        public decimal TransferAmount { get; set; }

    }
}
