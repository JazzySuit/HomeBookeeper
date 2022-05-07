using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class FictionBook : Book
{

	public FictionBook(
		string title,
		Isbn isbn)
		: base(title, isbn)
	{

	}
}
