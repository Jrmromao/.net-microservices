﻿using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Infra.IoC
{
   public  class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // domain bus
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
