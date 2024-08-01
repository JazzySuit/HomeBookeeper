using FluentAssertions;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.UnitTests.Fixture;
using HomeBookeper.Domain.Values;
using Xunit;

namespace HomeBookeper.Domain.UnitTests;

public class LibraryTests : IClassFixture<LibraryTestFixture>
{
	private readonly LibraryTestFixture _libraryTestFixture;

	public LibraryTests(LibraryTestFixture libraryTestFixture)
	{
		_libraryTestFixture = libraryTestFixture;
	}

	[Fact]
	public void Given_a_new_book_when_adding_it_to_the_library_then_the_book_should_be_able_to_be_found_by_its_isbn()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var emptyBookList = library.FindBook(book.Isbn);
		emptyBookList.Should().NotBeNull();
		emptyBookList.Should().BeEmpty();

		var addBookToLibrary = () => library.AddNewBook(book, user);
		addBookToLibrary.Should().NotThrow();

		var addedBook = library.FindBook(book.Isbn);

		addedBook.Should().NotBeNull();
		addedBook.Count().Should().Be(1);
		addedBook.First().Should().BeEquivalentTo(book);
	}
	[Fact]
	public void Given_a_new_book_when_adding_it_to_the_library_then_the_book_should_be_able_to_be_found_by_its_title()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var emptyBookList = library.FindBook(new Title(book.Title));
		emptyBookList.Should().NotBeNull();
		emptyBookList.Should().BeEmpty();

		library.AddNewBook(book, user);
		var addedBook = library.FindBook(new Title(book.Title));

		addedBook.Should().NotBeNull();
		addedBook.Count().Should().Be(1);
		addedBook.First().Should().BeEquivalentTo(book);
	}

	[Fact]
	public void Given_a_new_book_when_adding_it_to_the_library_then_the_book_should_be_able_to_be_found_by_its_author()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var booksAuthor = book.Authors.First();

		var emptyBookList = library.FindBook(new AuthorName(booksAuthor.FirstName, booksAuthor.LastName));
		emptyBookList.Should().NotBeNull();
		emptyBookList.Should().BeEmpty();

		library.AddNewBook(book, user);
		
		var addedBook = library.FindBook(new AuthorName(booksAuthor.FirstName, booksAuthor.LastName));

		addedBook.Should().NotBeNull();
		addedBook.Count().Should().Be(1);
		addedBook.First().Should().BeEquivalentTo(book);
	}

	[Fact]
	public void Given_a_duplicate_book_when_added_to_the_library_then_the_book_does_not_get_added_again()
	{
		// TODO: future feature - to allow adding multiple of the same books
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		library.AddNewBook(book, user);

		var addBookToLibrary = () => library.AddNewBook(book, user);
		addBookToLibrary.Should().NotThrow();

		var addedBook = library.FindBook(book.Isbn);
		addedBook.Count().Should().Be(1);
		addedBook.First().Should().BeEquivalentTo(book);
	}

	[Fact]
	public void Given_a_new_book_when_it_is_added_to_the_library_then_the_book_is_available_from_the_library_and_ready_to_be_issued()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		library.AddNewBook(book, user);

		var bookState = library.GetBookState(book);
		bookState.Should().Be(LibraryBookState.IsAnAvailableBook);

		book.CanBeIssued.Should().BeTrue();
	}

	[Fact]
	public void Given_an_available_book_to_loan_when_loaning_out_a_book_then_the_book_state_is_updated_to_onloan_and_who_it_is_loaned_to()
	{
		// is the book lent out & to who?
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();
		library.AddNewBook(book, user);

		var availableBook = library.FindBook(book.Isbn).Single();
		availableBook.Should().NotBeNull();
		availableBook.Should().BeEquivalentTo(book);
		library.GetBookState(availableBook!).Should().Be(LibraryBookState.IsAnAvailableBook);
		availableBook!.CanBeIssued.Should().BeTrue();

		library.LoanBook(book, user);

		var loanedBook = library.FindBook(book.Isbn).Single();
		loanedBook.Should().NotBeNull();
		loanedBook.Should().BeEquivalentTo(book);
		library.GetBookState(loanedBook!).Should().Be(LibraryBookState.IsAnAvailableBook);
		library.GetBookState(availableBook!).Should().Be(LibraryBookState.IsAnAvailableBook);
		loanedBook!.CanBeIssued.Should().BeFalse();
	}

	[Fact]
	public void Given_a_user_has_a_book_on_loan_when_they_try_to_borrow_the_book_again_then_there_is_no_change_in_book_state()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		library.AddNewBook(book, user);
		var initialTransactionCount = book.TransactionLog.Count;

		library.LoanBook(book, user);
		var firstBookState = library.GetBookState(book);
		book.CanBeIssued.Should().BeFalse();
		book.TransactionLog.Count.Should().Be(initialTransactionCount + 1);

		library.LoanBook(book, user);
		var secondBookState = library.GetBookState(book);
		secondBookState.Should().Be(firstBookState);
		book.CanBeIssued.Should().BeFalse();
		book.TransactionLog.Count.Should().Be(initialTransactionCount + 1);
	}

	[Fact]
	public void Given_a_book_is_on_loan_when_someone_tries_to_borrow_the_book_then_the_user_cannot_loan_out_the_book()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");
		ILibraryUser userAlice = new LibraryUser("Alice", "Wonderland");

		ILibrary library = new Library();

		library.AddNewBook(book, new LibraryUser("Library", "Admin"));
		var initialTransactionCount = book.TransactionLog.Count;
		book.CanBeIssued.Should().BeTrue();

		library.LoanBook(book, userJoe);
		book.CanBeIssued.Should().BeFalse();
		book.TransactionLog.Count.Should().Be(initialTransactionCount + 1);

		var firstBookState = library.GetBookState(book);
		
		library.LoanBook(book, userAlice);
		book.TransactionLog.Count.Should().Be(initialTransactionCount + 1);

		var secondBookState = library.GetBookState(book);
		secondBookState.Should().Be(firstBookState);
	}

	[Fact]
	public void Given_a_book_that_is_on_loan_when_the_book_is_returned_then_the_books_is_available()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		library.AddNewBook(book, new LibraryUser("Library", "Admin"));
		book.CanBeIssued.Should().BeTrue();
		var initialTransactionCount = book.TransactionLog.Count;

		library.LoanBook(book, userJoe);
		book.CanBeIssued.Should().BeFalse();
		book.TransactionLog.Count.Should().Be(initialTransactionCount + 1);

		library.ReturnBook(book, userJoe);
		book.CanBeIssued.Should().BeTrue();
		book.TransactionLog.Count.Should().Be(initialTransactionCount + 2);
	}

	[Fact]
	public void Given_a_book_that_is_on_loan_when_the_book_is_returned_then_the_book_can_be_issued_again()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");
		ILibraryUser userAlice = new LibraryUser("Alice", "Wonderland");

		ILibrary library = new Library();

		library.AddNewBook(book, new LibraryUser("Library", "Admin"));
		
		var initialTransactionCount = book.TransactionLog.Count;

		library.LoanBook(book, userJoe);
		library.ReturnBook(book, userJoe);

		library.LoanBook(book, userAlice);
		book.CanBeIssued.Should().BeFalse();
		book.TransactionLog.Count.Should().Be(initialTransactionCount + 3);
	}

	[Fact]
	public void Given_a_book_is_available_when_the_book_is_removed_from_the_library_then_the_book_cannot_be_issued()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		library.AddNewBook(book, new LibraryUser("Library", "Admin"));

		book.CanBeIssued.Should().BeTrue();

		library.RemoveBook(book, userJoe);

		var postState = library.GetBookState(book);
		postState.Should().Be(LibraryBookState.RemovedFromLibrary);
		book.CanBeIssued.Should().BeFalse();
	}

	//[Fact]
	public void Given_a_book_has_been_removed_from_the_library_when_issuing_the_book_to_a_user_then_issuing_the_book_fails_and_the_state_remains_unchanged()
	{
		Assert.False(true);
	}

	//[Fact]
	public void Given_a_book_has_been_removed_from_the_library_when_returning_the_book_then_returning_the_book_fails_and_the_state_remains_unchanged()
	{
		Assert.False(true);
	}

	public void Given_a_book_has_been_removed_from_the_library_when_adding_the_book_back_into_the_library_then_the_book_is_available_again()
	{
		Assert.False(true);
	}
}
