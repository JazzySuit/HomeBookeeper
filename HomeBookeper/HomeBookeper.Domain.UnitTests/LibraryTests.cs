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
		ILibraryBook book = new LibraryBook(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			new Isbn10(1234567890),
			"Book Publisher");

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
	public void Given_a_user_has_a_book_on_loan_when_they_try_to_borrow_again_then_there_is_no_change_in_book_state()
	{
		var isbn10 = new Isbn10 (1234567890);
		ILibraryBook book = new LibraryBook(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibraryUser user = new LibraryUser("Joe", "Bloggs");
		ILibrary library = new Library();

		library.AddNewBook(book, user);
		library.LoanBook(book, user);
		var firstBookState = library.GetBookState(book);

		library.LoanBook(book, user);
		var secondBookState = library.GetBookState(book);
		secondBookState.Should().Be(firstBookState);
	}

	[Fact]
	public void Given_a_book_is_on_loan_when_someone_tries_to_borrow_the_book_then_the_user_cannot_loan_out_the_book_and_the_state_and_record_are_unchanged()
	{
		var isbn10 = new Isbn10 (9876543210);

		ILibraryBook book = new LibraryBook(
			"Book Title",
			new Author("Ben", "Boss"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibraryUser userJoe = new LibraryUser("Joe", "Bloggs");
		ILibraryUser userAlice = new LibraryUser("Alice", "Wonderland");

		ILibrary library = new Library();

		library.AddNewBook(book, new LibraryUser("Library", "Admin"));
		library.LoanBook(book, userJoe);

		var firstBookState = library.GetBookState(book);

		library.LoanBook(book, userAlice);

		var secondBookState = library.GetBookState(book);

		secondBookState.Should().Be(firstBookState);
	}

	//[Fact]
	public void Returning_a_book_that_is_on_loan_sets_the_books_state_back_to_in_possession()
	{
		Assert.False(true);
	}

	//[Fact]
	public void A_book_that_needs_to_be_repaired_has_its_state_set_to_needs_repair()
	{
		Assert.False(true);
	}

	//[Fact]
	public void When_a_book_is_repaired_the_books_state_is_set_to_in_possession()
	{
		Assert.False(true);
	}

	//[Fact]
	public void Selling_a_book_sets_a_books_state_to_sold()
	{
		Assert.False(true);
	}

	//[Fact]
	public void A_book_that_has_been_sold_cannot_be_set_back_to_any_other_book_state()
	{
		Assert.False(true);
	}

	//[Fact]
	public void Giving_away_a_book_sets_a_books_state_to_given_away()
	{
		Assert.False(true);
	}

	//[Fact]
	public void A_book_that_has_been_given_away_cannot_be_set_back_to_any_other_book_state()
	{
		Assert.False(true);
	}

	//[Fact]
	public void Destroying_a_book_sets_a_books_state_to_destroyed()
	{
		Assert.False(true);
	}

	//[Fact]
	public void A_book_that_has_been_destroyed_cannot_be_set_back_to_any_other_book_state()
	{
		Assert.False(true);
	}
}
