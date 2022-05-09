using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class Book : BaseEntity
{
	public Book(
		string title,
		BookType type,
		Isbn isbn,
		string publisher,
		//Author? author = null,
		string? series = null)
	{
		if (string.IsNullOrEmpty(title))
		{
			throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));
		}

		Title = title;
		Type = type;
		Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn));
		Publisher = publisher;

		//AddAuthor(author);
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
		if(author is not null)
			_authors.Add(author);
	}

	private readonly List<Author> _authors = new List<Author>();
}
