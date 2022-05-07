using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Application.Features.Books.Queries.GetAllBooks;

public class GetAllBooksViewModel
{
	public int Id { get; init; }

	public string Title { get; init; }

	//public IReadOnlyCollection<Author> Authors { get; init; }

	public Isbn Isbn { get; init; }
}
	
