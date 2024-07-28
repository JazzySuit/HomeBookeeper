using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;

namespace HomeBookeper.Domain.Entities;

public class LibraryTransaction : Transaction<ILibraryBook>
{
	private LibraryTransaction(ILibraryBook book, ILibraryUser actioningUser, LibraryTransactionType action)
		: base(book, actioningUser)
	{
		Action = action;
	}

	public static LibraryTransaction BookAddedToLibrary(
		ILibraryBook book, 
		ILibraryUser actioningUser)
	{
		return new (book, actioningUser, LibraryTransactionType.BookAdded);
	}

	public static LibraryTransaction BookRemovedFromLibrary(
		ILibraryBook book,
		ILibraryUser actioningUser)
	{
		return new(book, actioningUser, LibraryTransactionType.BookRemoved);
	}

	public LibraryTransactionType Action { get; init; }
}
