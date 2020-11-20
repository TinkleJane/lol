using MediatR;

namespace ShaunaVayne.Infrastructure.Query
{
    public interface IQueryHandler<T, TResponse> : IRequestHandler<T, TResponse> where T : IRequest<TResponse>
    {

    }

}