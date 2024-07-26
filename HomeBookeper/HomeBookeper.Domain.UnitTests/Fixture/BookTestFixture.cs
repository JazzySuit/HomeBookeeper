using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.UnitTests.Fixture;

public class BookTestFixture : IDisposable
{
	public BookTestFixture()
	{
		ValidAuthor = new Author("Jane", "Doe");
		ValidBookTitle = "The book of tests";
		ValidBookPublisher = "Book Publisher";
	}

	public void Dispose()
	{
		
	}

	public Author ValidAuthor { get; init; }

	public string ValidBookTitle { get; init; }

	public string ValidBookPublisher { get; init; }

	public Isbn GenerateValidIsbn10() => new Isbn10(1234567890);

	public Isbn GenerateValidIsbn13() => new Isbn13(1234567899123);
}
