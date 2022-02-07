﻿using AutoMapper;
using NetReact.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NetReact.Application.Posts.Queries;
using NetReact.Application.Posts.Commands;
using NetReact.Application.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using NetReact.Domain.Filter;

namespace NetReact.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public PostController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("id")]
		public async Task<IActionResult> Get(int id)
		{
			var post = await _mediator.Send(new GetPostQuery { Id = id });
			var result = _mapper.Map<PostDto>(post);

			return Ok(result);
		}

		[HttpGet]
		[ApiExceptionFilter]
		public async Task<IActionResult> GetAll([FromQuery] PostsFilter filter)
		{
			GetPostsQuery query = _mapper.Map<GetPostsQuery>(filter);
			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreatePostCommand command)
		{
			var post = await _mediator.Send(command);
			var result = _mapper.Map<PostDto>(post);

			return Ok(result);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> Patch(int id, [FromBody] UpdatePostCommand command)
		{
			var post = await _mediator.Send(command);

			var result = _mapper.Map<PostDto>(post);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] ReplacePostCommand command)
		{
			var post = await _mediator.Send(command);

			var result = _mapper.Map<PostDto>(post);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _mediator.Send(new DeletePostCommand { Id = id });

			return NoContent();
		}

		[HttpGet("conditions")]
		public async Task<IActionResult> GetConditions()
		{
			var result = await _mediator.Send(new GetPostConditionsQuery());

			return Ok(result);
		}
	}
}