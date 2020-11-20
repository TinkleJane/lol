using ShaunaVayne.Bus.Command;
using ShaunaVayne.Models;

namespace ShaunaVayne.UICommands.General
{
    public class DeleteCommand<T> : ICommand<T> where T : Entity
    {
        public T Value { get; set; }
    }
}
