﻿using AutoMapper;
using NetReact.Domain;
using NetReact.Domain.DTOs;
using NetReact.Domain.Filter;
using NetReact.Domain.Interfaces;
using NetReact.Domain.Models;
using NetReact.Domain.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetReact.Application.Posts.Queries
{
     public class GetPostsQueryHandler :  IRequestHandler<GetPostsQuery, PagedResponse<PostDto>>
     {
          private readonly IPostRepository _postRepository;
          private readonly IMapper _mapper;

          public GetPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
          {
               _postRepository = postRepository;
               _mapper = mapper;
          }

          public Task<PagedResponse<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
          {
               var includes = new List<Expression<Func<Post, Object>>>();
               var predicates = new List<Expression<Func<Post, bool>>>();

               if (request.IncludeBook)
                    includes.Add(p => p.Book);

               if (request.IncludePostedBy)
                    includes.Add(p => p.PostedBy);

               if (request.Condition != null)
                    predicates.Add(p => p.Condition == request.Condition);

               if (request.PostedById != null)
                    predicates.Add(p => p.PostedById == request.PostedById);

               if (request.Status != null)
                    predicates.Add(p => p.Status.ToString() == request.Status);

               if (request.BookId != null)
                    predicates.Add(p => p.BookId == request.BookId);

               var paginationRequestFilter = _mapper.Map<PaginationFilter>(request);

               var posts = _postRepository.GetPagedData<PostDto>(predicates, includes, paginationRequestFilter, _mapper);

               return Task.FromResult(posts);
          }
     }
}
