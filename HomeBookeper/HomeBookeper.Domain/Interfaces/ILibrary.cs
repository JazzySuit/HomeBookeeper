using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibrary
{
	void AddBook(IBook book);

	IBook? FindBook(Isbn isbnNumber);
}