using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities.Books;

public class AvailableBook : ILibraryBookType
{
	internal static AvailableBook Create(NewBook book) => new AvailableBook(book);

	internal static AvailableBook Return(IssuedBook book) => new AvailableBook(book);

	internal static AvailableBook ReturnRemoved(RemovedBook book) => new AvailableBook(book);

	private AvailableBook(ILibraryBookType book)
	{
		Isbn = book.Isbn;
		Title = book.Title;
		_authors.AddRange(book.Authors);
	}

	public Isbn Isbn { get; init; }

	public Title Title { get; init; }

	public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

	private readonly List<Author> _authors = new ();
}
