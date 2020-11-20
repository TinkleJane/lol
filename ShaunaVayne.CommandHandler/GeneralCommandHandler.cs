using MediatR;
using ShaunaVayne.Bus.Command;
using ShaunaVayne.Data;
using ShaunaVayne.UICommands.General;
using ShaunaVayne.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ShaunaVayne.CommandHandler
{
    public class GeneralCommandHandler<T> : ICommandHandler<T> where T:Entity
    {
        private readonly DemaciaContext _context;
        public GeneralCommandHandler(DemaciaContext context)
        {
            _context = context;
        }

        public async Task Handle(ICommand<T> command)
        {
            if (command is AddCommand<T>)
            {
                _context.Set<T>().Add(command.Value);
            }
            if (command is EditCommand<T>)
            {
                _context.Set<T>().Update(command.Value);
            }
            if (command is DeleteCommand<T>)
            {
                _context.Set<T>().Remove(command.Value);
            }
            await _context.SaveChangesAsync();
        }
    }
}
