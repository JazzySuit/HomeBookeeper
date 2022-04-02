namespace HomeBookeper.Domain.Entities;

public class NonFictionBook : Book
{
	public NonFictionBook(string title, Author author)
		: base(title, author)
	{
	}
}
