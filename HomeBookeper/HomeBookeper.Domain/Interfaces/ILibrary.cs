using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibrary
{
	void AddNewBook(ILibraryBook book, ILibraryUser addedByUser);

	BookStateRecord GetBookRecord(ILibraryBook book);

	BookState GetBookState(IBook book);

	public IBook FindBook(SearchableBookProperties searchProp);

	void LoanBook(ILibraryBook book, ILibraryUser user);

	void WishlistBook(WishlistedBook book, ILibraryUser wishlistedByUser);
}