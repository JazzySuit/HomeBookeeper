using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.UnitTests.Fixture;

public class BookTestFixture : IDisposable
{
	public BookTestFixture()
	{
		ValidBookTitle = "The book of tests";
		ValidBookPublisher = "Book Publisher";
	}

	public void Dispose()
	{
		
	}

	public string ValidBookTitle { get; init; }

	public string ValidBookPublisher { get; set; }

	public Isbn GenerateValidIsbn(IsbnStandard isbnStandard = IsbnStandard.Isbn13)
		=> isbnStandard switch
		{
			IsbnStandard.Isbn10 => new Isbn(IsbnStandard.Isbn10, 1234567890),
			IsbnStandard.Isbn13 => new Isbn(IsbnStandard.Isbn13, 1234567899123),
			_ => throw new NotImplementedException("Isbn standard not implemented")
		};
}
