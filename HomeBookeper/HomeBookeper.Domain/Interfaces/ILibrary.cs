using HomeBookeper.Domain.Entities.Books;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibrary
{
	AvailableBook AddNewBook(NewBook book, ILibraryUser addedByUser);

	ILibraryBookType? GetBook(ILibraryBookType book);

	IEnumerable<ILibraryBookType> FindBook(SearchableBookProperties searchProp);

	IssuedBook LoanBook(AvailableBook book, ILibraryUser user);

	AvailableBook ReturnBook(IssuedBook book, ILibraryUser user);

	RemovedBook RemoveBook(ILibraryBookType book, ILibraryUser user);
}