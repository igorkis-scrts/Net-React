﻿using BookExchange.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookExchange.Domain.DTOs
{
     public class BookDto
     {
          public int Id { get; set; }
          public string Title { get; set; }
          public string Isbn { get; set; }
          public string ShortDescription { get; set; }
          public string ThumbnailPath { get; set; }
          public BookDetailsDto Details { get; set; }
          public List<AuthorDto> Authors { get; set; }
          public List<CategoryDto> Categories { get; set; }
     }
}
