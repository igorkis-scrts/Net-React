﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetReact.Domain.Models
{
     public enum PostStatus { 
          Active, 
          Disabled
     }

     public enum Condition
     {
          New,
          Good,
          Acceptable,
     }

     public class Post : BaseEntity
     {
          public int BookId { get; set; }
          public int? PostedById { get; set; }
          public PostStatus Status { get; set; }
          public DateTime TimeAdded { get; set; }

          public virtual Book Book { get; set; }
          public virtual User PostedBy { get; set; }
          public virtual Condition Condition { get; set; }
          public virtual ICollection<User> BookmarkedBy { get; set; }
     }
}

