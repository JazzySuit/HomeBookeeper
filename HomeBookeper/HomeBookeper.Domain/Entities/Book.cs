using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class Book : BaseEntity, IBook
{
	public Book(
		string title,
		Author author,
		BookType type,
		Isbn isbn,
		string publisher, 
		string? series = null)
	{
		if (string.IsNullOrEmpty(title))
		{
			throw new InvalidBookException($"'{nameof(title)}' cannot be null or empty.");
		}

		if (author == null)
		{
			throw new InvalidBookException($"'{nameof(author)}' cannot be null.");
		}

		if (string.IsNullOrEmpty(publisher))
		{
			throw new InvalidBookException($"'{nameof(publisher)}' cannot be null or empty.");
		}

		Title = title;
		Type = type;
		Isbn = isbn ?? throw new InvalidIsbnException(nameof(isbn));
		Publisher = publisher;

		Series = series;
	}

	public string Title { get; init; }
	
	public BookType Type { get; init; }

	public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

	//public ICollection<Illustrator> Illustrators { get; private set; }

	public string Publisher { get; init; }
	
	public string? Series { get; init; }

	public Isbn Isbn { get; init; }

	public void AddAuthor(Author author)
	{
		if (author is not null)
		{
			_authors.Add(author);
		}
	}

	public void AddAuthors(ICollection<Author> authors)
	{
		foreach (var author in authors)
		{
			AddAuthor(author);
		}
	}

	private readonly List<Author> _authors = new List<Author>();

}
