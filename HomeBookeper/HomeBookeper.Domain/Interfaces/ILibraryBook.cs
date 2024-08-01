using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibraryBook : IBook
{
	Isbn Isbn { get; }

	string Publisher { get; }

	int PublishedYear { get; }

	BookType Type { get; }

	IReadOnlyCollection<BookTransaction> TransactionLog { get; } // does this need to be??

	bool CanBeIssued { get; }

	void IssueTo(ILibraryUser user);

	void Returned(ILibraryUser user);
}

public interface IBook 
{
	string Title { get; }

	IReadOnlyCollection<Author> Authors { get; }
}

