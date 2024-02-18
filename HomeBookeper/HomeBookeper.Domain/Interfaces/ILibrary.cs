using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibrary
{
	void AddNewBook(IBook book, ILibraryUser addedByUser);

	BookStateRecord GetBookRecord(IBook book);

	BookState GetBookState(IBook book);

	IBook? FindBook(Isbn isbnNumber);

	void LoanBook(IBook book, ILibraryUser user);

	void WishlistBook(IBook book, ILibraryUser wishlistedByUser);
}