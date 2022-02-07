﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookExchange.Domain.DTOs
{
     public class BookDetailsDto
     {
          public string Description { get; set; }
          public string Publisher { get; set; }
          public string ImagePath { get; set; }
          public int? PublishedYear { get; set; }
          public int? PageCount { get; set; }
     }
}
