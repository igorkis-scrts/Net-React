﻿using BookExchange.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookExchange.Infrastructure.Persistence.Configurations
{
     public class PostConfig : IEntityTypeConfiguration<Post>
     {
          public void Configure(EntityTypeBuilder<Post> builder)
          {
               builder.Property(x => x.TimeAdded)
                    .HasDefaultValueSql("getdate()");

               builder.Property(x => x.Status)
                    .HasDefaultValue(PostStatus.Active)
                    .HasConversion(
                         x => x.ToString(),
                         x => (PostStatus)Enum.Parse(typeof(PostStatus), x));

               builder.Property(x => x.Condition)
                    .HasConversion<string>()
                    .HasColumnType("varchar")
                    .HasMaxLength(50);
          }
     }
}
