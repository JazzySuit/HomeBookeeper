using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class ChildrensBook : Book
{
	public ChildrensBook(
		string title,
		Isbn isbn)
		: base(title, isbn)
	{
	}

	//public Illustrator Illustrator { get; set; }
}
