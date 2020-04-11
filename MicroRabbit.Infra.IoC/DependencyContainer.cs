using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Servives;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Banking.Data.Repository;

namespace MicroRabbit.Infra.IoC
{
   public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // domain bus
            services.AddTransient<IEventBus, RabbitMQBus>();
            // Application services 
            services.AddTransient<IAccountService, AccountService>();
            //data
            services.AddTransient<IAccountRepository, AccountRepository>();
      
        
        }
    }
}
