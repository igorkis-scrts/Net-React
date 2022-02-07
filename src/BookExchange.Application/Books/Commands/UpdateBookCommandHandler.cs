﻿using BookExchange.Domain.Interfaces;
using BookExchange.Domain.Models;
using BookExchange.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Books.Commands
{
     class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
     {
          private readonly IBookRepository _bookRepository;
          private readonly IRepositoryBase<Author> _bookAuthorsRepository;
          private readonly ICategoryRepository _bookCategoriesRepository;

          public UpdateBookCommandHandler(IBookRepository bookRepository, IRepositoryBase<Author> bookAuthorsRepository, ICategoryRepository bookCategoriesRepository)
          {
               _bookRepository = bookRepository;
               _bookAuthorsRepository = bookAuthorsRepository;
               _bookCategoriesRepository = bookCategoriesRepository;
          }

          public Task<Book> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
          {
               var book = _bookRepository.GetByIdWithInclude(command.Id, b => b.Details, b => b.Authors, b => b.Categories);

               if (book == null)
               {
                     throw new NotFoundException(nameof(Book), command.Id);
               }

               if (!string.IsNullOrWhiteSpace(command.Title))
                    book.Title = command.Title;

               if (!string.IsNullOrWhiteSpace(command.ShortDescription))
                    book.ShortDescription = command.ShortDescription;

               if (!string.IsNullOrWhiteSpace(command.Isbn))
                    book.Isbn = command.Isbn;

               if (!string.IsNullOrWhiteSpace(command.Description))
                    book.Details.Description = command.Description;

               if (!string.IsNullOrWhiteSpace(command.Publisher))
                    book.Details.Publisher = command.Publisher;


               if (command.AuthorIds != null)
                    command.AuthorIds.ForEach(id => {
                         var author = _bookAuthorsRepository.GetById(id);

                         if (author != null) {
                              book.Authors.Add(author);
                         }
                    });

               if (command.CategoryIds != null)
                    command.CategoryIds.ForEach(id => {
                         var category = _bookCategoriesRepository.GetById(id);

                         if (category != null) {
                              book.Categories.Add(category);
                         }
                    });

               _bookRepository.SaveAll();

               return Task.FromResult<Book>(book);
          }

     }
}
