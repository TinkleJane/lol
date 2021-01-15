using ShaunaVayne.Bus.Command;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShaunaVayne.UICommands.Account
{
    public class CreateAccountCommand : IMediatRCommand
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "两次输入的密码不一致")]
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