using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class Library : ILibrary
{
	public void AddBook(IBook book)
	{
		_books.Add(book);
	}

	public IBook? FindBook(Isbn isbnNumber)
	{
		return _books.Where(book => book.Isbn == isbnNumber).SingleOrDefault();
	}

	private readonly List<IBook> _books = new();
}
