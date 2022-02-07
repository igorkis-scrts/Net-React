﻿using BookExchange.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchange.Application.Categories.Commands
{
     public class CreateCategoryCommand : IRequest<Category>
     {
          public string Name { get; set; }
     }
}
