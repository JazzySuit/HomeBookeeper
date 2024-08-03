using HomeBookeper.Domain.Entities.Books;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;

namespace HomeBookeper.Domain.Common;

public class BookTransaction : Transaction<ILibraryBook>
{
	private BookTransaction(ILibraryBook book, ILibraryUser actioningUser, BookTransactionType action)
		: base(book, actioningUser)
	{
		Action = action;
	}

	public static BookTransaction BookIssuedToUser(
		ILibraryBook book,
		ILibraryUser actioningUser)
	{
		return new(book, actioningUser, BookTransactionType.BookIssued);
	}

	public static BookTransaction BookReturnedByUser(
		ILibraryBook book,
		ILibraryUser actioningUser)
	{
		return new(book, actioningUser, BookTransactionType.BookReturned);
	}

	public BookTransactionType Action { get; init; }
}

public class NewBookTransaction : Transaction<NewBook>
{
	private NewBookTransaction(NewBook book, ILibraryUser actioningUser, LibraryTransactionType action)
		: base(book, actioningUser)
	{
		Action = action;
	}

	public static NewBookTransaction BookAddedToLibrary(
		NewBook book,
		ILibraryUser actioningUser)
	{
		return new(book, actioningUser, LibraryTransactionType.BookAdded);
	}

	public LibraryTransactionType Action { get; init; }
}