using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShaunaVayne.Bus.Command;
using ShaunaVayne.Data;
using ShaunaVayne.Models;
using ShaunaVayne.UICommands.Account;

namespace ShaunaVayne.CommandHandler.Account
{
    public class AccountCommandHandler : IMediatRCommandHandler<CreateAccountCommand, Unit>
    {
        private readonly DemaciaContext _context;

        public AccountCommandHandler(DemaciaContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        } 
    }


    public class BookCommandHandler : IMediatRCommandHandler<AddBookCommand, Unit>, 
        IMediatRCommandHandler<EditBookCommand, Unit>,
        IMediatRCommandHandler<DeleteBookCommand, Unit>
    {
        private readonly DemaciaContext _context;

        public BookCommandHandler(DemaciaContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book { Name = request.Name };
            await _context.Books.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(EditBookCommand request, CancellationToken cancellationToken)
        {
            var book = _context.Books.Single(x => x.Id == request.Id);
            book.Name = request.Name;
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = _context.Books.Single(x => x.Id == request.Id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

    }
}