using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class WishlistedBook : IBook
{
	public WishlistedBook(string title)
	{
		Title = title;
	}

	public string Title { get; init; }

	public Isbn? Isbn { get; init; }

	public IReadOnlyCollection<Author> Authors => throw new NotImplementedException();
}