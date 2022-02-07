﻿using BookExchange.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookExchange.Application.Books.Commands
{
     public class UpdateBookCommand : IRequest<Book>
     {
          [Required]
          public int Id { get; set; }
          
          public string Title { get; set; }

          [StringLength(13, MinimumLength = 13, ErrorMessage = "Invalid ISBN length")]
          public string Isbn { get; set; }
          
          public string ShortDescription { get; set; }
          
          public string Description { get; set; }
          
          public string Publisher { get; set; }
          
          public int? PageCount { get; set; }
          
          public int? PublishedYear { get; set; }
          
          public List<int> AuthorIds { get; set; }
          
          public List<int> CategoryIds { get; set; }
     }
}
