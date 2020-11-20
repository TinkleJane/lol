using MediatR;
using ShaunaVayne.Models;

namespace ShaunaVayne.Bus.Command
{
    public interface IMediatRCommand : IRequest
    {

    }

    public interface IMediatRCommand<out T> : IRequest<T>
    {

    }

    public interface ICommand<T> where T: Entity
    {
        T Value { get; set; }
    }

}