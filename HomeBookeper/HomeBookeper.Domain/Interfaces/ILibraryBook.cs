using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibraryBook : IBook
{
	Isbn Isbn { get; }

	string Publisher { get; }

	int PublishedYear { get; }

	BookType Type { get; }

	IReadOnlyCollection<ILibraryTransaction> TransactionLog { get; }
}

public interface IBook 
{
	string Title { get; }

	IReadOnlyCollection<Author> Authors { get; }
}

