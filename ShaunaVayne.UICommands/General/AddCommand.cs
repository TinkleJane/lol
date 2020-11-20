using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShaunaVayne.Bus.Command;
using ShaunaVayne.Models;

namespace ShaunaVayne.UICommands.General
{
    public class AddCommand<T> : ICommand<T> where T : Entity
    {
        public T Value { get; set; }
    }
}
