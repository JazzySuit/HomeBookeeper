using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;

namespace HomeBookeper.Domain.Entities;

public record BookStateRecord //: IBookState
{
	public BookStateRecord(IBook book, BookState state)
	{
		Metadata = new Dictionary<string, (string, string)>();
		Book = book;
		State = state;
	}

	public IBook Book { get; init; }

	public BookState State { get; init; }

	/// <summary>
	/// key [metadata name] => (value, data type)
	/// </summary>
	public IDictionary<string, (string, string)> Metadata { get; init; }
}