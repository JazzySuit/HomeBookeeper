namespace HomeBookeper.Domain.Values;

public record Isbn
{
	public IsbnStandard Standard { get; init; }

	public long Value { get; init; }
}

public enum IsbnStandard
{

	Isbn10,
	Isbn13
}