﻿using NetReact.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetReact.Application.Posts.Commands
{
     public class ReplacePostCommand : IRequest<Post>
     {
          [Required]
          public int Id { get; set; }
          [Required]
          public int BookId { get; set; }
          [Required]
          public int PostedById { get; set; }
          [Required]
          public string Condition { get; set; }
          public int Status { get; set; }
     }
}
