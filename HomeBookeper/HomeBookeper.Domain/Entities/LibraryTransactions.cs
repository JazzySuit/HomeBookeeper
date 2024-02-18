using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public abstract class LibraryBookTransaction : ILibraryTransaction
{
	public LibraryBookTransaction(Isbn isbn, ILibraryUser actioningUser)
	{
		Id = Guid.NewGuid();
		ActionedOn = DateTime.Now;
		ActionedBy = actioningUser.Name;
		Value = isbn;
	}

	public Guid Id { get; init; }

	public virtual TransactionType Type { get; init; }

	public DateTime ActionedOn { get; init; }

	public string ActionedBy { get; init; }

	public Isbn Value { get; init; }
}

public class LibraryBookAdded : LibraryBookTransaction, ILibraryTransaction
{
	public LibraryBookAdded(Isbn isbn, ILibraryUser actioningUser)
		: base(isbn, actioningUser)
	{
		Type = TransactionType.Added;
	}

	public override TransactionType Type { get; init; }
}

public class LibraryBookLoanedOut : LibraryBookTransaction, ILibraryTransaction
{
	public LibraryBookLoanedOut(Isbn isbn, ILibraryUser actioningUser)
		: base(isbn, actioningUser)
	{
		Type = TransactionType.LoanedOut;
	}

	public override TransactionType Type { get; init; }
}

public class LibraryBookWishlisted : LibraryBookTransaction, ILibraryTransaction
{
	public LibraryBookWishlisted(Isbn isbn, ILibraryUser actioningUser)
		: base(isbn, actioningUser)
	{
		Type = TransactionType.Wishlisted;
	}

	public override TransactionType Type { get; init; }
}