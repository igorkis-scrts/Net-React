﻿using NetReact.Domain.DTOs;
using NetReact.Domain.Interfaces;
using NetReact.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetReact.Application.Categories.Queries
{
     public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
     {
          private readonly ICategoryRepository _categoriesRepository;

          public GetCategoriesQueryHandler(ICategoryRepository categoriesRepository)
          {
               _categoriesRepository = categoriesRepository;
          }

          public Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
          {
               return Task.FromResult(_categoriesRepository.GetAll());
          }
     }
}
