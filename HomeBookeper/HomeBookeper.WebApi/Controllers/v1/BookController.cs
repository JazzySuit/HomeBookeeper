using HomeBookeper.Application.Features.Books.Commands.CreateBook;
using HomeBookeper.Application.Features.Books.Queries.GetAllBooks;
using HomeBookeper.Application.Features.Books.Queries.GetBooksById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookeper.WebApi.Controllers.v1;

[ApiVersion("1.0")]
public class BookController : BaseApiController
{
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] GetAllBooksParameter filter)
	{
		return Ok(await Mediator.Send(
			new GetAllBooksQuery()
			{
				PageSize = filter.PageSize,
				PageNumber = filter.PageNumber
			}));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		return Ok(await Mediator.Send(new GetBookByIdQuery { Id = id }));
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Post(CreateBookCommand command)
	{
		return Ok(await Mediator.Send(command));
	}

	[HttpPut("{id}")]
	public void Put(int id, [FromBody] string value)
	{
	}
}
