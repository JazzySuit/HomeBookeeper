using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.UnitTests.Fixture;

public class LibraryTestFixture
{
	private readonly Random _random;

	public LibraryTestFixture()
	{
		_random = new Random();
	}

	public ILibraryBook CreateAValidBook(string title = "A valid book", bool isIsbn13 = false)
		=> new LibraryBook(
					title,
					new Author("Bobb", "Bearly"),
					BookType.FictionBook,
					isIsbn13 ? new Isbn13(RandomIsbn13()) : new Isbn10(RandomIsbn10()),
					"Book Publisher");

	private long RandomIsbn10()
		=> LongRandom(min: 1000000000, max: 9999999999);

	private long RandomIsbn13()
		=> LongRandom(min: 1000000000000, max: 9999999999999);

	private long LongRandom(long min, long max)
	{
		long result = _random.Next((int)(min >> 32), (int)(max >> 32));
		result = (result << 32) | (long)_random.Next((int)min, (int)max);
		return result;
	}
}