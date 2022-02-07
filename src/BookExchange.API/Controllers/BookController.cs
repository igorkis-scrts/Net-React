﻿using AutoMapper;
using BookExchange.Application.Books.Commands;
using BookExchange.Application.Books.Queries;
using BookExchange.Domain.Commands;
using BookExchange.Domain.DTOs;
using BookExchange.Domain.Filter;
using BookExchange.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookExchange.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public BookController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id, bool includeDetails)
		{
			var book = await _mediator.Send(new GetBookQuery { Id = id, IncludeDetails = includeDetails });
			var bookDto = _mapper.Map<BookDto>(book);

			return Ok(bookDto);
		}


		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BooksFilter bookFilter)
		{
			var query = _mapper.Map<GetBooksQuery>(bookFilter);
			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpGet("smart-search")]
		public async Task<IActionResult> GetAll([FromQuery] string searchTerm)
		{
			SmartSearchBooksQuery query = new SmartSearchBooksQuery { SearchTerm = searchTerm };
			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateBookCommand command)
		{
			var book = await _mediator.Send(command);
			var result = _mapper.Map<BookDto>(book);

			return CreatedAtAction(nameof(Get), new { id = book.Id }, result);
		}


		[HttpPatch("{id}")]
		public async Task<IActionResult> Patch(int id, [FromBody] UpdateBookCommand command)
		{
			if (id != command.Id)
			{
				BadRequest();
			}

			var book = await _mediator.Send(command);
			var result = _mapper.Map<BookDto>(book);

			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] ReplaceBookCommand command)
		{
			if (id != command.Id)
			{
				BadRequest();
			}

			await _mediator.Send(command);

			return NoContent();
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _mediator.Send(new DeleteBookByIdCommand { Id = id });
			return NoContent();
		}
	}
}