using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public abstract class Book : BaseEntity
{
	public Book(string title, 
		Isbn isbn)
	{
		Title = title;
		Isbn = isbn;
	}

	public string Title { get; init; }

	//public ICollection<Author> Authors { get; private set; }

	// TODO: have metadata on a book series
	//public Series Series

	public Isbn Isbn { get; init; }
}
