using MediatR;

namespace ShaunaVayne.Infrastructure.Query
{
    public interface IQuery<out T> : IRequest<T>
    {

    }

}