using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;
using System.Linq;

namespace HomeBookeper.Domain.Entities;

public class Library : ILibrary
{
	public void AddNewBook(ILibraryBook book, ILibraryUser addedByUser)
	{
		if (_libraryBooks.Contains(book))
			return;

		_libraryBooks.Add(book);

		RemoveBookFromWishlist(book, addedByUser);

		_libraryBookTransactions.Add(new LibraryBookAdded(book, addedByUser));
	}

	private void RemoveBookFromWishlist(ILibraryBook book, ILibraryUser user)
	{
		// TODO: handle same name books, but different books
		var wishlistedBook = _wishlistedBooks
			.Where(b => b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase))
			.FirstOrDefault();

		if (wishlistedBook is not null)
		{
			_wishlistedBooks.Remove(wishlistedBook);
			_libraryBookTransactions.Add(new WishlistedBookRemoved(wishlistedBook, user));
		}
	}

	public BookStateRecord GetBookRecord(IBook book)
	{
		var bookState = GetBookState(book);

		return bookState switch
		{
			_ => throw new NotImplementedException()
		};
	}

	public BookState GetBookState(IBook book)
	{
		throw new NotImplementedException();
	}

	public IBook FindBook(SearchableBookProperties searchProp)
	{
		throw new NotImplementedException();
	}

	public ILibraryBook? FindBook(Isbn isbnNumber)
		=> _libraryBooks.Where(book => book.Isbn == isbnNumber).SingleOrDefault();

	// TODO: might want to change return type...
	public void LoanBook(ILibraryBook book, ILibraryUser user)
	{
		// if the book is able to be loaned out
		//   then loan the book
		// else 
		//   do nothing ...??

		if (IsAbleToBeBorrowed(book))
		{
			_libraryBooksOnLoan.Add(user, new List<ILibraryBook> { book });
			_libraryBookTransactions.Add(new LibraryBookLoanedOut(book, user));
		}
	}

	private bool IsAbleToBeBorrowed(ILibraryBook book)
	{
		return !_libraryBooksOnLoan.Any(loans => loans.Value.Contains(book));
	}

	public void WishlistBook(WishlistedBook book, ILibraryUser wishlistedByUser)
	{
		if (_wishlistedBooks.Contains(book) || _libraryBooks.Any(b => b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase)))
			return;

		_wishlistedBooks.Add(book);
		_booksWishedToBeAddedToLibrary.Add(new BookWishlisted(book, wishlistedByUser));
	}

	public BookStateRecord GetBookRecord(ILibraryBook book)
	{
		throw new NotImplementedException();
	}

	private readonly List<ILibraryBook> _libraryBooks = new();
	private readonly List<WishlistedBook> _wishlistedBooks = new();
	private readonly List<ILibraryTransaction> _libraryBookTransactions = new();
	private readonly List<BookWishlisted> _booksWishedToBeAddedToLibrary = new ();

	private readonly Dictionary<ILibraryUser, List<ILibraryBook>> _libraryBooksOnLoan = new();
}