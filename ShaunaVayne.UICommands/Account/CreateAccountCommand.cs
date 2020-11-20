using ShaunaVayne.Bus.Command;
using System;

namespace ShaunaVayne.UICommands.Account
{
    public class CreateAccountCommand : IMediatRCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class AddBookCommand: IMediatRCommand
    {
        public string Name { get; set; }
    }

    public class EditBookCommand : IMediatRCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class DeleteBookCommand : IMediatRCommand
    {
        public Guid Id { get; set; }
    }
}