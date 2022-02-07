﻿using NetReact.Application.Common.Exceptions;
using NetReact.Domain.Interfaces;
using NetReact.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetReact.Application.Authors.Commands
{
     public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
     {
          private readonly IRepositoryBase<Author> _authorsRepository;

          public DeleteAuthorCommandHandler(IRepositoryBase<Author> authorsRepository)
          {
               _authorsRepository = authorsRepository;
          }

          public Task<Unit> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
          {
               var author = _authorsRepository.Delete(command.Id);
               _authorsRepository.SaveAll();

               if (author == null)
               {
                    throw new NotFoundException(nameof(Author), command.Id);
               }

               return Task.FromResult(Unit.Value);
          }
     }
}
