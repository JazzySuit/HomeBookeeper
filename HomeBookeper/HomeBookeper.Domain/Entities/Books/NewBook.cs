using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities.Books;

public class NewBook : ILibraryBookType
{
	public static NewBook Create(Isbn isbn, BookTitle title, Author author) => new(isbn, title, author);

	public static NewBook Create(Isbn isbn, string title, Author author)
	{
		var bookTitle = new BookTitle(title);

		return Create(isbn, bookTitle, author);
	}

	public static NewBook Create(Isbn isbn, string title, string authorFirstName, string authorLastName)
	{
		var author = new Author(authorFirstName, authorLastName);

		return Create(isbn, title, author);
	}

	private NewBook(Isbn isbn, BookTitle title, Author author)
	{
		Isbn = isbn ?? throw new InvalidBookException($"The book {nameof(isbn)} cannot be null");
		Title = title ?? throw new InvalidBookException($"The books {nameof(title)} cannot be null");

		_authors.Add(author ?? throw new InvalidBookException($"The books {nameof(author)} cannot be null"));
	}

	public Isbn Isbn { get; init; }
	public BookTitle Title { get; init;  }

	public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

	public NewBook AddAuthor(Author author)
	{
		if (author is not null)
		{
			_authors.Add(author);
		}

		return this;
	}

	public void AddAuthors(ICollection<Author> authors)
	{
		foreach (var author in authors)
		{
			AddAuthor(author);
		}
	}

	private readonly List<Author> _authors = new ();
}
