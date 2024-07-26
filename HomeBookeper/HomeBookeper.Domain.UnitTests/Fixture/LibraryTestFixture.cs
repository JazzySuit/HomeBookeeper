using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.UnitTests.Fixture;

public class LibraryTestFixture
{
	public LibraryTestFixture()
	{

	}

	public ILibraryBook CreateAValidBook()
	{
		var isbn10 = new Isbn10(1234567890);

		return new LibraryBook(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");
	}
}