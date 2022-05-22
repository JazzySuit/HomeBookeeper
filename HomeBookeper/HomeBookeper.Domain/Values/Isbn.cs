using HomeBookeper.Domain.Exceptions;

namespace HomeBookeper.Domain.Values;

public record Isbn
{
	public Isbn(
		IsbnStandard standard,
		long value)
	{
		if (standard == IsbnStandard.Isbn10 
			&& value.ToString().Length != 10)
		{
			throw new InvalidIsbnException($"The ISBN-10 {nameof(value)}, {value}, is not of length 10");
		}

		if (standard == IsbnStandard.Isbn13
			&& value.ToString().Length != 13)
		{
			throw new InvalidIsbnException($"The ISBN-13 {nameof(value)}, {value}, is not of length 13");
		}

		Standard = standard;
		Value = value;
	}

	public IsbnStandard Standard { get; init; }

	public long Value { get; init; }
}

public enum IsbnStandard
{
	Isbn10,
	Isbn13
}