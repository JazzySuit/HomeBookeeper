using HomeBookeper.Domain.Entities.Books;
using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.Extensions;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class Library : ILibrary
{
	public AvailableBook AddNewBook(NewBook book, ILibraryUser addedByUser)
	{
		var addedBook = GetBook(book) switch
		{
			null => AddBook(book, addedByUser),
			AvailableBook a => a,
			IssuedBook i => throw new InvalidBookException($"The book, {book.Title}, has already been added to the library. It is not currently issued to {i.IssuedTo}"),
			RemovedBook r => AddBook(r, addedByUser),
			_ => throw new InvalidBookException($"The book, {book.Title}, is in an unexpected state when issuing the book")
		};

		return addedBook;
	}

	private AvailableBook AddBook(NewBook book, ILibraryUser addedByUser)
	{
		var makeBookAvailable = AvailableBook.Create(book);
		_libraryBooks.Add(makeBookAvailable);
		_libraryBookTransactions.Add(LibraryTransaction.BookAddedToLibrary(book, addedByUser));

		return makeBookAvailable;
	}

	private AvailableBook AddBook(RemovedBook book, ILibraryUser addedByUser)
	{
		_libraryBooks.Remove(book);

		var makeBookAvailable = AvailableBook.ReturnRemoved(book);
		_libraryBooks.Add(makeBookAvailable);
		_libraryBookTransactions.Add(LibraryTransaction.BookAddedToLibrary(book, addedByUser));

		return makeBookAvailable;
	}

	public ILibraryBookType? GetBook(ILibraryBookType book) 
		=> _libraryBooks.Where(b => b.Isbn == book.Isbn).SingleOrDefault();

	public IEnumerable<ILibraryBookType> FindBook(SearchableBookProperties searchProp)
	{
		return searchProp switch
		{
			SearchTitle title => FindBookByTitle(title),
			Isbn isbn => FindBookByIsbn(isbn),
			SearchAuthor author => FindBookByAuthor(author),
			_ => new List<ILibraryBookType>()
		};
	}

	private IEnumerable<ILibraryBookType> FindBookByAuthor(SearchAuthor author)
		=> _libraryBooks.Where(b => b.Authors.Where(a => a.FirstName == author.First && a.LastName == author.Last).Any());

	private IEnumerable<ILibraryBookType> FindBookByTitle(SearchTitle title)
		=> _libraryBooks.Where(b => b.Title.AsSearchable() == title);

	private IEnumerable<ILibraryBookType> FindBookByIsbn(Isbn isbnNumber)
		=> _libraryBooks.Where(book => book.Isbn == isbnNumber);

	public IssuedBook LoanBook(AvailableBook book, ILibraryUser user)
	{
		var issuedBook = GetBook(book) switch
		{
			AvailableBook a => IssueBook(a, user),
			IssuedBook i => i,
			RemovedBook r => throw new InvalidBookException($"Cannot issue the book, {r.Title}, as it has been removed from the library."),
			_ => throw new InvalidBookException($"The book, {book.Title}, is in an unexpected state when issuing the book")
		};

		return issuedBook;
	}

	private IssuedBook IssueBook(AvailableBook book, ILibraryUser user)
	{
		var issuedBook = IssuedBook.Create(book, user);
		_libraryBooks.Remove(book);
		_libraryBooks.Add(issuedBook);

		return issuedBook;
	}

	public AvailableBook ReturnBook(IssuedBook book, ILibraryUser user)
	{
		var issuedBook = GetBook(book) switch
		{
			AvailableBook a => a,
			IssuedBook i => ReturnIssuedBook(i, user),
			RemovedBook r => throw new InvalidBookException($"Cannot issue the book, {r.Title}, as it has been removed from the library."),
			_ => throw new InvalidBookException($"The book, {book.Title}, is in an unexpected state when issuing the book")
		};

		return issuedBook;
	}

	private AvailableBook ReturnIssuedBook(IssuedBook issuedBook, ILibraryUser user)
	{
		var returnedBook = AvailableBook.Return(issuedBook);
		_libraryBooks.Remove(issuedBook);
		_libraryBooks.Add(returnedBook);

		return returnedBook;
	}

	public RemovedBook RemoveBook(ILibraryBookType book, ILibraryUser user)
	{
		var libraryBook = GetBook(book) switch
		{
			AvailableBook a => RemoveAvailableBook(a, user),
			IssuedBook i => RemoveIssuedBook(i, user),
			RemovedBook r => r,
			_ => throw new InvalidBookException($"The book, {book.Title}, is in an unexpected state when issuing the book")
		};

		return libraryBook;
	}

	private RemovedBook RemoveAvailableBook(AvailableBook book, ILibraryUser user)
	{
		var removedBook = RemovedBook.Remove(book);
		_libraryBooks.Remove(book);
		_libraryBooks.Add(removedBook);

		return removedBook;
	}

	private RemovedBook RemoveIssuedBook(IssuedBook book, ILibraryUser user)
	{
		var removedBook = RemovedBook.Remove(book);
		_libraryBooks.Remove(book);
		_libraryBooks.Add(removedBook);

		return removedBook;
	}

	private readonly List<ILibraryBookType> _libraryBooks = new();
	private readonly List<LibraryTransaction> _libraryBookTransactions = new();
}