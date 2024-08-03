using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;

namespace HomeBookeper.Domain.Entities;

public class LibraryTransaction : Transaction<ILibraryBookType>
{
	private LibraryTransaction(ILibraryBookType book, ILibraryUser actioningUser, LibraryTransactionType action)
		: base(book, actioningUser)
	{
		Action = action;
	}

	public static LibraryTransaction BookAddedToLibrary(
		ILibraryBookType book, 
		ILibraryUser actioningUser)
	{
		return new (book, actioningUser, LibraryTransactionType.BookAdded);
	}

	public static LibraryTransaction BookRemovedFromLibrary(
		ILibraryBookType book,
		ILibraryUser actioningUser)
	{
		return new(book, actioningUser, LibraryTransactionType.BookRemoved);
	}

	public LibraryTransactionType Action { get; init; }
}
