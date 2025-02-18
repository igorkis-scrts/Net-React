﻿using AutoMapper;
using NetReact.Application.Users.Commands;
using NetReact.Application.Users.Queries;
using NetReact.Domain;
using NetReact.Domain.DTOs;
using NetReact.Domain.Filter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetReact.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize("ApiScope")]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public UserController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("current-user")]
		public async Task<IActionResult> GetCurrentUser()
		{
			var user = await _mediator.Send(new GetCurrentUserQuery());
			var result = _mapper.Map<UserDto>(user);

			return Ok(result);
		}


		[HttpPost]
		public async Task<IActionResult> CreateUser()
		{
			await _mediator.Send(new CreateUserCommand());

			return Ok();
		}

		[HttpGet("id")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _mediator.Send(new GetUserQuery());
			var result = _mapper.Map<UserDto>(user);

			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _mediator.Send(new GetUsersQuery());
			var result = _mapper.Map<ICollection<UserDto>>(users);

			return Ok(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUser()
		{
			await _mediator.Send(new DeleteUserCommand());

			return NoContent();
		}

		[HttpGet("{id}/stats")]
		public async Task<IActionResult> GetUserStats(int id)
		{
			var response = await _mediator.Send(new GetUserStatsQuery { UserId = id });

			return Ok(response);
		}

		[HttpGet("{id}/books/wished")]
		public async Task<IActionResult> GetWishedBooks(int id, [FromQuery] BooksFilter filter)
		{
			var query = _mapper.Map<GetUserWishedBooksQuery>(filter);
			query.UserId = id;
			var response = await _mediator.Send(query);

			return Ok(response);
		}

		[HttpPost("{id}/books/wished")]
		public async Task<IActionResult> AddBookToWishlist(int id, [FromBody] WishListDto wishlistDto)
		{
			var response = await _mediator.Send(new AddToWishlistCommand { UserId = id, BookId = wishlistDto.BookId });

			return Ok(response);
		}


		[HttpGet("{id}/posts/owned")]
		public async Task<IActionResult> GetUserActivePosts(int id, [FromQuery] PaginationFilter filter)
		{
			var query = _mapper.Map<GetUserPostsQuery>(filter);
			query.UserId = id;

			var response = await _mediator.Send(query);

			return Ok(response);
		}
	}
}