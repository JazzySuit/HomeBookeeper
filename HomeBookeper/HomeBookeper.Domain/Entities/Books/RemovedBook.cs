using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities.Books;

public class RemovedBook : ILibraryBookType
{
	internal static RemovedBook Remove(AvailableBook book) => new(book);

	internal static RemovedBook Remove(IssuedBook book) => new(book);

	private RemovedBook(ILibraryBookType book)
	{
		Isbn = book.Isbn;
		Title = book.Title;
		_authors.AddRange(book.Authors);
	}

	public Isbn Isbn { get; init; }

	public Title Title { get; init; }

	public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

	private readonly List<Author> _authors = new();
}
