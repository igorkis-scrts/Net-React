﻿namespace BookExchange.Domain.Models
{
     public class Wishlist : BaseEntity
     {
          public int UserId { get; set; }
          public int BookId { get; set; }
          public virtual User User { get; set; }
          public virtual Book Book { get; set; }
     }
}
