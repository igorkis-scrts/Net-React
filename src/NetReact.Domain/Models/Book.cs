﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetReact.Domain.Models
{
     public class Book : BaseEntity
     {
          public string Title { get; set; }
          public string Isbn { get; set; }
          public string ShortDescription { get; set; }
          public string ThumbnailPath { get; set; }
          public BookDetails Details { get; set; }
          public virtual ICollection<User> WishedBy { get; set; }
          public virtual ICollection<Author> Authors { get; set; }
          public virtual ICollection<BookAuthor> BookAuthor { get; set; }
          public virtual ICollection<Category> Categories { get; set; }
     }
}
