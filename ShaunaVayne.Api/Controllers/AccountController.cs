using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ShaunaVayne.UICommands.Account;
using MediatR;
using System;
using ShaunaVayne.Models;
using ShaunaVayne.Bus;
using ShaunaVayne.UICommands.General;

namespace ShaunaVayne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        
        private readonly ILogger<AccountController> _logger;
        private readonly IBus _bus;

        public AccountController(ILogger<AccountController> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }


        [HttpPost]
        [Route("create")]
        public void Create(CreateAccountCommand command)
        {
            _bus.Send(command);
        }


        [HttpGet]
        [Route("test")]
        public async Task TestAsync()
        {
            var command = new CreateAccountCommand { UserName = "moto", Password = "123456", ConfirmPassword = "123456" };
            await _bus.Send(command);
        }

        [HttpPost]
        [Route("book/add")]
        public async Task AddBook(AddCommand<Book> command)
        {
            await _bus.Send(command);
        }

        //[HttpGet]
        //[Route("book/edit")]
        //public async Task EditBook()
        //{
        //    var command = new EditBookCommand { Name = "Moto" };
        //    await _mediator.Send(command);
        //}

        //[HttpGet]
        //[Route("book/delete/{id}")]
        //public async Task DeleteBook(Guid id)
        //{
        //    var command = new DeleteBookCommand { Id = id };
        //    await _mediator.Send(command);
        //}
    }
}
