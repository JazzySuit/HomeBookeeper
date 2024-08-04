using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities.Books;

public class IssuedBook : ILibraryBookType
{
	internal static IssuedBook Create(AvailableBook book, ILibraryUser user) => new(book, user);

	private IssuedBook(AvailableBook book, ILibraryUser user)
	{
		Isbn = book.Isbn;
		Title = book.Title;
		_authors.AddRange(book.Authors);

		IssuedTo = user;
	}

	public ILibraryUser IssuedTo { get; init; }

	public Isbn Isbn { get; init; }

	public Title Title { get; init; }

	public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

	private readonly List<Author> _authors = new ();
}
