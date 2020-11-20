using ShaunaVayne.Bus.Command;
using ShaunaVayne.Models;
using System.Threading.Tasks;

namespace ShaunaVayne.Bus
{
    public interface IBus
    {
        Task Send<T>(ICommand<T> command) where T: Entity;
        Task Send(IMediatRCommand command);
    }
}
