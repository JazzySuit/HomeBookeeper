using HomeBookeper.Domain.Exceptions;

namespace HomeBookeper.Domain.Values;


public abstract record Isbn : SearchableBookProperties
{
	public long Value { get; init; }
}

public record Isbn10 : Isbn
{
	public Isbn10(long value)
	{
		if (value.ToString().Length != 10)
			throw new InvalidIsbnException($"The ISBN-10 {nameof(value)}, {value}, is not of length 10");

		Value = value;
	}
}

public record Isbn13 : Isbn
{
	public Isbn13(long value)
	{
		if (value.ToString().Length != 13)
			throw new InvalidIsbnException($"The ISBN-13 {nameof(value)}, {value}, is not of length 13");

		Value = value;
	}
}