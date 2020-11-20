using MediatR;
using ShaunaVayne.Bus.Command;
using ShaunaVayne.Models;
using System;
using System.Threading.Tasks;

namespace ShaunaVayne.Bus
{
    public class InMemoryBus : IBus
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;

        public InMemoryBus(IMediator mediator,IServiceProvider serviceProvider)
        {
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        public async Task Send<T>(ICommand<T> command) where T:Entity
        {
            var commandHandler = (ICommandHandler<T>)_serviceProvider.GetService(typeof(ICommandHandler<T>));
            await commandHandler.Handle(command);
        }

        public async Task Send(IMediatRCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
