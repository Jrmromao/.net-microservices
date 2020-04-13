using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Servives;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.DependencyInjection;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Infra.Bus;

using MediatR;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.CommandHandlers;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //// domain bus
            services.AddTransient<IEventBus, RabbitMQBus>();
            //// Application services 
            services.AddTransient<IAccountService, AccountService>();
            ////data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();

            //IOC for command handlers tranfer
            //Domain Banking commands 
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TranferCommandHandler>();

        }
    }
}
