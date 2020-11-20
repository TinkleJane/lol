using ShaunaVayne.Bus.Command;
using ShaunaVayne.Models;

namespace ShaunaVayne.UICommands.General
{
    public class EditCommand<T> : ICommand<T> where T : Entity
    {
        public T Value { get; set; }
    }
}
