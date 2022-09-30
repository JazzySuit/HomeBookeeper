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

	public Isbn GenerateValidIsbn(IsbnStandard isbnStandard = IsbnStandard.Isbn13)
		=> isbnStandard switch
		{
			IsbnStandard.Isbn10 => new Isbn(IsbnStandard.Isbn10, 1234567890),
			IsbnStandard.Isbn13 => new Isbn(IsbnStandard.Isbn13, 1234567899123),
			_ => throw new NotImplementedException("Isbn standard not implemented")
		};
}
