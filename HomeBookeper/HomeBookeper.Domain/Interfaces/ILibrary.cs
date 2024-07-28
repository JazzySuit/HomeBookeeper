using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibrary
{
	void AddNewBook(ILibraryBook book, ILibraryUser addedByUser);

	LibraryBookState GetBookState(ILibraryBook book);

	IEnumerable<ILibraryBook> FindBook(SearchableBookProperties searchProp);

	void LoanBook(ILibraryBook book, ILibraryUser user);

	void ReturnBook(ILibraryBook book);

	(BookStatus BookStatus, string ToUser) GetLibraryBookState(ILibraryBook book);
}