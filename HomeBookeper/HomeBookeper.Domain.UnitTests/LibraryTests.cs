using FluentAssertions;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Entities.Books;
using HomeBookeper.Domain.Extensions;
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
		var book = _libraryTestFixture.CreateANewBook();
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
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var emptyBookList = library.FindBook(book.Title.AsSearchable());
		emptyBookList.Should().NotBeNull();
		emptyBookList.Should().BeEmpty();

		library.AddNewBook(book, user);
		var addedBook = library.FindBook(book.Title.AsSearchable());

		addedBook.Should().NotBeNull();
		addedBook.Count().Should().Be(1);
		addedBook.First().Should().BeEquivalentTo(book);
	}

	[Fact]
	public void Given_a_new_book_when_adding_it_to_the_library_then_the_book_should_be_able_to_be_found_by_its_author()
	{
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var searchAuthor = book.Authors.First().AsSearchAble();

		var emptyBookList = library.FindBook(searchAuthor);
		emptyBookList.Should().NotBeNull();
		emptyBookList.Should().BeEmpty();

		library.AddNewBook(book, user);
		
		var addedBook = library.FindBook(searchAuthor);

		addedBook.Should().NotBeNull();
		addedBook.Count().Should().Be(1);
		addedBook.First().Should().BeEquivalentTo(book);
	}

	[Fact]
	public void Given_a_duplicate_book_when_added_to_the_library_then_the_book_does_not_get_added_again()
	{
		// TODO: future feature - to allow adding multiple of the same books
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var addedBook = library.AddNewBook(book, user);

		var addingBookAgain = () => library.AddNewBook(book, user);
		addingBookAgain.Should().NotThrow();

		var availableBook = library.FindBook(book.Isbn);
		availableBook.Count().Should().Be(1);
		availableBook.First().Should().Be(addedBook);
	}

	[Fact]
	public void Given_a_new_book_when_it_is_added_to_the_library_then_the_book_is_available_from_the_library_and_ready_to_be_issued()
	{
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		var addedBook = library.AddNewBook(book, user);

		var availableBook = library.GetBook(book);
		availableBook.Should().BeOfType(typeof(AvailableBook));
		addedBook.Should().BeEquivalentTo(availableBook);
	}

	[Fact]
	public void Given_an_available_book_when_loaning_the_available_book_then_the_book_state_is_updated_to_issued_and_who_it_is_loaned_to()
	{
		// is the book lent out & to who?
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();
		
		var availableBook = library.AddNewBook(book, user);
		availableBook.Should().NotBeNull();

		var issuedBook = library.LoanBook(availableBook!, user);
		issuedBook.Should().NotBeNull();

		var bookIssuedTo = library.GetBook(issuedBook!) switch
		{
			IssuedBook i => i.IssuedTo,
			_ => throw new Exception($"Wrong type returned. Expected an {typeof(IssuedBook)} type.")
		};
		bookIssuedTo.Should().NotBeNull();
		bookIssuedTo.Should().BeEquivalentTo(user);
	}

	[Fact]
	public void Given_a_user_has_a_book_on_loan_when_they_try_to_borrow_the_book_again_then_there_is_no_change_in_the_book_state()
	{
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var availableBook = library.AddNewBook(book, user);

		var firstLoan = library.LoanBook(availableBook, user);
		firstLoan.Should().NotBeNull();
		
		var firstStateA = library.GetBook(availableBook);
		var firstStateB = library.GetBook(firstLoan);
		firstStateA.Should().NotBeNull();
		firstStateA.Should().BeEquivalentTo(firstStateB);

		var secondLoan = library.LoanBook(availableBook, user);
		secondLoan.Should().NotBeNull();

		var secondState = library.GetBook(secondLoan);
		secondState.Should().BeEquivalentTo(firstStateB);

		secondLoan.Should().Be(firstLoan);
	}

	[Fact]
	public void Given_a_book_is_issued_when_someone_tries_to_borrow_the_book_then_the_user_is_not_issued_the_book()
	{
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");
		ILibraryUser userAlice = new LibraryUser("Alice", "Wonderland");
		ILibrary library = new Library();

		var availableBook = library.AddNewBook(book, new LibraryUser("Library", "Admin"));
		
		var issuedBook = library.LoanBook(availableBook, userJoe);
		var firstBookState = library.GetBook(issuedBook);
		
		var issuedBookAttempt = library.LoanBook(availableBook, userAlice);
		var secondBookState = library.GetBook(issuedBookAttempt);

		issuedBook.IssuedTo.Should().Be(userJoe);
		issuedBook.Should().Be(issuedBookAttempt);
		secondBookState.Should().Be(firstBookState);
	}

	[Fact]
	public void Given_a_book_that_is_issued_when_the_book_is_returned_then_the_book_is_available()
	{
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var availableBook = library.AddNewBook(book, new LibraryUser("Library", "Admin"));
		
		var issuedBook = library.LoanBook(availableBook, userJoe);
		
		var returnedBook = library.ReturnBook(issuedBook, userJoe);
		returnedBook.Should().BeEquivalentTo(availableBook);
	}

	[Fact]
	public void Given_a_book_that_is_issued_when_the_book_is_returned_then_the_book_can_be_issued_again()
	{
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");
		ILibraryUser userAlice = new LibraryUser("Alice", "Wonderland");
		ILibrary library = new Library();

		var availableBook = library.AddNewBook(book, new LibraryUser("Library", "Admin"));

		var issuedBook = library.LoanBook(availableBook, userJoe);

		var returnedBook = library.ReturnBook(issuedBook, userJoe);

		var issuedAgain = library.LoanBook(returnedBook, userAlice);
		issuedAgain.Should().NotBeNull();
		issuedAgain.IssuedTo.Should().Be(userAlice);
	}

	[Fact]
	public void Given_a_book_is_available_when_the_book_is_removed_from_the_library_then_the_book_state_is_updated_on_search()
	{
		var book = _libraryTestFixture.CreateANewBook();
		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		var availableBook = library.AddNewBook(book, new LibraryUser("Library", "Admin"));

		var removedBook = library.RemoveBook(availableBook, userJoe);
		removedBook.Should().NotBeNull();

		var searchedBook = library.GetBook(removedBook);
		searchedBook.Should().NotBeNull();
		searchedBook.Should().Be(removedBook);
	}

	[Fact]
	public void Given_a_book_has_been_removed_from_the_library_when_adding_the_book_to_the_library_then_the_book_is_available_again()
	{
		var book = _libraryTestFixture.CreateANewBook("A removed book returned");
		ILibraryUser userJoe = new LibraryUser("Jim", "Bobby");
		ILibrary library = new Library();

		var availableBook = library.AddNewBook(book, new LibraryUser("Library", "Admin"));

		var removedBook = library.RemoveBook(availableBook, userJoe);

		var removeBookAdded = library.AddNewBook(book, new LibraryUser("Library", "Admin"));
		removeBookAdded.Should().NotBeNull();
		removeBookAdded.Should().BeEquivalentTo(availableBook);
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
