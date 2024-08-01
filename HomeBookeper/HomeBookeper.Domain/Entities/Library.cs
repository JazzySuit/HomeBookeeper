using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class Library : ILibrary
{
	public void AddNewBook(ILibraryBook book, ILibraryUser addedByUser)
	{
		if (_libraryBooks.Contains(book))
			return;

		_libraryBooks.Add(book);
		_libraryBookTransactions.Add(LibraryTransaction.BookAddedToLibrary(book, addedByUser));
	}

	public LibraryBookState GetBookState(ILibraryBook book)
	{
		if (_libraryBooks.Contains(book))
			return LibraryBookState.IsAnAvailableBook;
		else if (_libraryBookTransactions
					.Where(t => t.Value.Isbn == book.Isbn)
					.Where(t => t.Action == LibraryTransactionType.BookRemoved)
					.LastOrDefault() is not null)
			return LibraryBookState.RemovedFromLibrary;

		return LibraryBookState.NotInLibrary;
	}

	public IEnumerable<ILibraryBook> FindBook(SearchableBookProperties searchProp)
	{
		return searchProp switch
		{
			Title title => FindBookByTitle(title),
			Isbn isbn => FindBookByIsbn(isbn),
			AuthorName author => FindBookByAuthor(author),
			_ => new List<ILibraryBook>()
		};
	}

	private IEnumerable<ILibraryBook> FindBookByAuthor(AuthorName author)
		=> _libraryBooks.Where(b => b.Authors.Where(a => a.FirstName == author.First && a.LastName == author.Last).Any());

	private IEnumerable<ILibraryBook> FindBookByTitle(Title title)
		=> _libraryBooks.Where(b => b.Title == title.T);

	private IEnumerable<ILibraryBook> FindBookByIsbn(Isbn isbnNumber)
		=> _libraryBooks.Where(book => book.Isbn == isbnNumber);

	// TODO: might want to change return type...
	public void LoanBook(ILibraryBook book, ILibraryUser user)
	{
		if (book.CanBeIssued)
		{
			book.IssueTo(user);
		}
	}

	public void ReturnBook(ILibraryBook book, ILibraryUser user)
	{
		// assumption: if book cannot be issued, then is is onloan/issued
		if(!book.CanBeIssued)
		{
			book.Returned(user);
		}
	}

	public void RemoveBook(ILibraryBook book, ILibraryUser user)
	{
		if(_libraryBooks.Contains(book))
		{
			_libraryBooks.Remove(book);
			_libraryBookTransactions.Add(LibraryTransaction.BookRemovedFromLibrary(book, user));
		}
	}

	private readonly List<ILibraryBook> _libraryBooks = new();
	private readonly List<LibraryTransaction> _libraryBookTransactions = new();
}