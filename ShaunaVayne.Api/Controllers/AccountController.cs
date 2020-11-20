using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ShaunaVayne.UICommands.Account;
using MediatR;
using System;
using ShaunaVayne.Models;

namespace ShaunaVayne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        
        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;

        public AccountController(ILogger<AccountController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPost]
        [Route("create")]
        public void Create(CreateAccountCommand command)
        {
            _mediator.Send(command);
        }


        [HttpGet]
        [Route("test")]
        public void Test()
        {
            var command = new CreateAccountCommand { UserName = "moto", Password = "123456", ConfirmPassword = "123456" };
            _mediator.Send(command);
        }

        [HttpGet]
        [Route("book/add")]
        public async Task AddBook()
        {
            //var command = new AddBookCommand { Name = "Learning C#" };
            //var command = new AddCommand<Book>
            //{
            //    Value = new Book { Name = "Professional C#"}
            //};
            //await _mediator.Send(command);
        }

        [HttpGet]
        [Route("book/edit")]
        public async Task EditBook()
        {
            var command = new EditBookCommand { Name = "Moto" };
            await _mediator.Send(command);
        }

        [HttpGet]
        [Route("book/delete/{id}")]
        public async Task DeleteBook(Guid id)
        {
            var command = new DeleteBookCommand { Id = id };
            await _mediator.Send(command);
        }
    }
}
