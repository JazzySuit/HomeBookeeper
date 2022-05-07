using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class NonFictionBook : Book
{
	public NonFictionBook(
		string title,
		Isbn isbn)
		: base(title, isbn)
	{
	}
}
