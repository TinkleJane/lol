using System.Threading.Tasks;

namespace ShaunaVayne.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        Task<string> CreateToken(string username);
    }
}