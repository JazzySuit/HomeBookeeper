using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class BoardBook : Book
{
	public BoardBook(
		string title, 
		Isbn isbn)
		: base(title, isbn)
	{
	}

	//public Illustrator Illustrator { get; set; }
}
