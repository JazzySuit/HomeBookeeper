using HomeBookeper.Application.Features.Books.Commands.CreateBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookeper.WebApi.Controllers.v1;

[ApiVersion("1.0")]
public class BookController : BaseApiController
{
	[HttpGet]
	public IEnumerable<string> Get()
	{
		return new string[] { "value1", "value2" };
	}

	[HttpGet("{id}")]
	public string Get(int id)
	{
		return "value";
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
