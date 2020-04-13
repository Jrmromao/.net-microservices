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
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Application.Servives;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.EventHandlers;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //// domain bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp => {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            // subscriptions
            services.AddTransient<TransferEventHandler>();

            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //// Application services 
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();
            ////data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransferRepository, TranferRepository>();
            services.AddTransient<BankingDbContext>();
            services.AddTransient<TransferDbContext>();

            //IOC for command handlers tranfer
            //Domain Banking commands 
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TranferCommandHandler>();
            
        }
    }
}
