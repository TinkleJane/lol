using MediatR;
using ShaunaVayne.Models;
using System.Threading.Tasks;

namespace ShaunaVayne.Bus.Command
{
    public interface IMediatRCommandHandler<T, TResponse> : IRequestHandler<T, TResponse> where T : IRequest<TResponse>
    {

    }

    public interface ICommandHandler<T> where T: Entity
    {
        Task Handle(ICommand<T> command);
    }


}