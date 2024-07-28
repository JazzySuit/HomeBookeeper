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

	public (BookStatus BookStatus, string ToUser) GetLibraryBookState(ILibraryBook book)
	{
		var bookInLib = FindBookByIsbn(book.Isbn).SingleOrDefault();

		return bookInLib is not null
			? (BookStatus.IsAvailable, string.Empty)
			: bookInLib?.CanBeIssued == true 
				? (BookStatus.IsAvailable, string.Empty)
				: (BookStatus.OnLoan, string.Empty) ;
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
		// if the book is able to be loaned out
		//   then loan the book
		// else 
		//   do nothing ...??

		if (IsAbleToBeBorrowed(book))
		{
			_libraryBooksOnLoan.Add(user, new List<ILibraryBook> { book });
			book.IssuedTo(user);
		}
	}

	private bool IsAbleToBeBorrowed(ILibraryBook book)
	{
		return !_libraryBooksOnLoan.Any(loans => loans.Value.Contains(book));
	}

	public void ReturnBook(ILibraryBook book)
	{
		throw new NotImplementedException();
	}

	private readonly List<ILibraryBook> _libraryBooks = new();
	private readonly List<WishlistedBook> _wishlistedBooks = new();
	private readonly List<LibraryTransaction> _libraryBookTransactions = new();

	private readonly Dictionary<ILibraryUser, List<ILibraryBook>> _libraryBooksOnLoan = new();
}