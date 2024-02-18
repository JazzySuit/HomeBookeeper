using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Entities;

public class Library : ILibrary
{
	public void AddNewBook(IBook book, ILibraryUser addedByUser)
	{
		if (_libraryBooks.Contains(book))
			return;

		_libraryBooks.Add(book);

		if (_wishlistedBooks.Contains(book))
			_wishlistedBooks.Remove(book);

		_transactions.Add(new LibraryBookAdded(book.Isbn, addedByUser));
	}

	public BookStateRecord GetBookRecord(IBook book)
	{
		var bookState = GetBookState(book);

		return bookState switch
		{
			BookState.OnLoan => OnLoanBookRecord(book)
		};
	}

	private BookStateRecord OnLoanBookRecord(IBook book)
	{
		var record = new BookStateRecord(book, BookState.OnLoan);

		// watchout: Can only do this filtering here because it has
		// already been done again but for checking book state
		var loanTransaction = _transactions
			.Where(t => t.Value == book.Isbn)
			.Where(t => t.Type == TransactionType.LoanedOut)
			.OrderByDescending(t => t.ActionedOn)
			.First();

		// WARNING - ASSUMPTION: only one user in library with name, no duplicates
		var loanUser = _libraryBooksOnLoan
			.Where(loans => loans.Value.Contains(book))
			.Where(loans => loans.Key.Name == loanTransaction.ActionedBy)
			.Select(loans => loans.Key)
			.Single();

		record.Metadata.Add("onloanto", (loanUser.Id.ToString(), nameof(Guid)));
		record.Metadata.Add("returnduedate", ("", ""));

		return record;
	}

	public BookState GetBookState(IBook book)
	{
		var transaction = _transactions
			.Where(t => t.Value == book.Isbn)
			.OrderByDescending(t => t.ActionedOn)
			.FirstOrDefault();

		return transaction?.Type switch
		{
			TransactionType.Wishlisted => BookState.OnWishlist,
			TransactionType.Added => BookState.InPossession,
			TransactionType.LoanedOut => BookState.OnLoan,
			_ => BookState.NotInLibrary
		};
	}

	public IBook? FindBook(Isbn isbnNumber)
	{
		var book = _libraryBooks.Where(book => book.Isbn == isbnNumber).SingleOrDefault();

		return book ?? _wishlistedBooks.Where(book => book.Isbn == isbnNumber).SingleOrDefault();
	}

	public void LoanBook(IBook book, ILibraryUser user)
	{
		_libraryBooksOnLoan.Add(user, new List<IBook> { book });

		_transactions.Add(new LibraryBookLoanedOut(book.Isbn, user));
	}

	public void WishlistBook(IBook book, ILibraryUser wishlistedByUser)
	{
		if (_wishlistedBooks.Contains(book) || _libraryBooks.Contains(book))
			return;

		_wishlistedBooks.Add(book);
		_transactions.Add(new LibraryBookWishlisted(book.Isbn, wishlistedByUser));
	}

	private readonly List<IBook> _libraryBooks = new();
	private readonly List<IBook> _wishlistedBooks = new();
	private readonly List<ILibraryTransaction> _transactions = new();

	private readonly Dictionary<ILibraryUser, List<IBook>> _libraryBooksOnLoan = new();
}