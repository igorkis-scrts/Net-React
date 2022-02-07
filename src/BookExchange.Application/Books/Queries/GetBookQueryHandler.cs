﻿using System.Threading;
using System.Threading.Tasks;
using BookExchange.Application.Common.Exceptions;
using BookExchange.Domain.Interfaces;
using BookExchange.Domain.Models;
using BookExchange.Domain.Queries;
using MediatR;

namespace BookExchange.Application.Books.Queries
{
     public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book>
     {
          private readonly IBookRepository _bookRepository;

          public GetBookQueryHandler(IBookRepository bookRepository)
          {
               _bookRepository = bookRepository;
          }

          public Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
          {
               Book book;

               if (!request.IncludeDetails) {
                    book = _bookRepository.GetById(request.Id);
               } else {
                    book = _bookRepository.GetByIdWithInclude(request.Id, b => b.Details, b => b.Categories, b=>b.Authors);
               }

               if (book == null)
               {
                    throw new NotFoundException(nameof(Book), request.Id);
               }

               return Task.FromResult(book);
          }
     }
}
