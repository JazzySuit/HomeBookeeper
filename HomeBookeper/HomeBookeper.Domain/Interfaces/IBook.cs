using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface IBook
{
	string Title { get; }

	IReadOnlyCollection<Author> Authors { get; }

	BookType Type { get; }

	Isbn Isbn { get; }

	string Publisher { get; }
}

//public interface IBookState
//{
//	IBook Book { get; }

//	BookState State { get; }

//	IDictionary<string, (string, string)> Metadata { get; }
//}