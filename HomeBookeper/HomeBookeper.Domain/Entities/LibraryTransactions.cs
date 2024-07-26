using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;

namespace HomeBookeper.Domain.Entities;

public abstract class LibraryBookTransaction<T> : ILibraryTransaction
{
	public LibraryBookTransaction(T transactionValue, ILibraryUser actioningUser)
	{
		Id = Guid.NewGuid();
		ActionedOn = DateTime.Now;
		ActionedBy = actioningUser.Name;
		Value = transactionValue;
	}

	public Guid Id { get; init; }

	public TransactionType Type { get; protected set; }

	public DateTime ActionedOn { get; private set; }

	public string ActionedBy { get; private set; }

	public T Value { get; private set; }
}



public class LibraryBookAdded : LibraryBookTransaction<ILibraryBook>, ILibraryTransaction
{
	public LibraryBookAdded(ILibraryBook book, ILibraryUser actioningUser)
		: base(book, actioningUser)
	{
		Type = TransactionType.Added;
	}
}

public class LibraryBookLoanedOut : LibraryBookTransaction<ILibraryBook>, ILibraryTransaction
{
	public LibraryBookLoanedOut(ILibraryBook book, ILibraryUser actioningUser)
		: base(book, actioningUser)
	{
		Type = TransactionType.LoanedOut;
	}
}

public class BookWishlisted : LibraryBookTransaction<IBook>, ILibraryTransaction
{
	public BookWishlisted(IBook book, ILibraryUser actioningUser)
		: base(book, actioningUser)
	{
		Type = TransactionType.Wishlisted;
	}
}

public class WishlistedBookRemoved : LibraryBookTransaction<IBook>, ILibraryTransaction
{
	public WishlistedBookRemoved(IBook book, ILibraryUser actioningUser)
		: base(book, actioningUser)
	{
		Type = TransactionType.WishlistRemoved;
	}
}