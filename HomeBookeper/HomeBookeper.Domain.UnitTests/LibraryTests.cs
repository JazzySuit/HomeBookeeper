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

	// Book state "diagram"
	// ## record cannot be deleted
	// book states: (+wishlist)->(+in possesion)<->(on loan, needs repair)->(sold, given away, destroyed)

	public LibraryTests(LibraryTestFixture libraryTestFixture)
	{
		_libraryTestFixture = libraryTestFixture;
	}


	[Fact]
	public void Given_a_new_book_when_adding_it_to_the_library_then_the_book_should_be_able_to_be_found()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		var addBookToLibrary = () => library.AddNewBook(book, user);

		addBookToLibrary.Should().NotThrow();

		var addedBook = library.FindBook(book.Isbn);

		addedBook.Should().NotBeNull();
		addedBook.Should().BeEquivalentTo(book);
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

		addedBook.Should().BeEquivalentTo(book);
	}

	[Fact]
	public void Given_a_new_book_when_it_is_added_to_the_library_then_it_has_a_book_state_that_is_in_possession()
	{
		ILibraryBook book = _libraryTestFixture.CreateAValidBook();
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		library.AddNewBook(book, user);

		var bookState = library.GetBookState(book);

		bookState.Should().Be(BookState.InPossession);
	}

	[Fact]
	public void Given_a_book_that_is_not_in_the_library_when_it_is_wishlisted_to_the_library_then_the_book_is_searchable_and_state_is_wishlisted()
	{
		var book = new WishlistedBook("A book I hope to read one day");
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		library.WishlistBook(book, user);

		var addedBook = library.FindBook(new Title(book.Title));

		addedBook.Should().NotBeNull();
		addedBook.Should().NotBeEquivalentTo(book);
		addedBook!.GetType().Should().Be(typeof(WishlistedBook));
	}

	[Fact]
	public void Given_a_book_that_has_not_been_wished_for_yet_and_is_not_in_the_library_when_wish_listed_for_then_the_book_can_be_searched_for_and_has_a_book_state_as_wishlist()
	{
		var book = new WishlistedBook("A book I hope to read one day");
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		library.WishlistBook(book, user);

		var bookState = library.GetBookState(book);

		bookState.Should().Be(BookState.OnWishlist);
	}

	[Fact]
	public void Given_a_book_that_already_exists_in_the_library_when_attempted_to_be_added_to_the_wishlist_then_it_state_is_unchanged()
	{
		var isbn10 = new Isbn10(1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		var wishlistBook = new WishlistedBook("A book I hope to read one day");
		ILibraryBook libraryBook = new LibraryBook(
			wishlistBook.Title,
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();
		library.AddNewBook(libraryBook, user);

		var priorBookState = library.GetBookState(libraryBook);
		priorBookState.Should().Be(BookState.InPossession);

		var wishlistAction = () => library.WishlistBook(wishlistBook, user);
		wishlistAction.Should().NotThrow();

		var postBookState = library.GetBookState(wishlistBook);
		postBookState.Should().Be(BookState.InPossession);
	}

	[Fact]
	public void Given_a_book_that_has_been_wishlisted_when_it_is_added_to_the_library_then_it_is_removed_from_the_wishlist_and_added_to_the_library_with_the_state_as_in_possession()
	{
		var isbn10 = new Isbn10(1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		var wishlistBook = new WishlistedBook("A book I hope to read one day");
		ILibraryBook book = new LibraryBook(
			wishlistBook.Title,
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		library.WishlistBook(wishlistBook, user);
		
		library.AddNewBook(book, user);

		var bookState = library.GetBookState(book);

		bookState.Should().Be(BookState.InPossession);
	}

	[Fact]
	public void Given_an_available_book_to_loan_when_loaning_out_a_book_then_the_book_state_is_updated_to_onloan_and_who_it_is_loaned_to()
	{
		// is the book lent out & to who?
		var isbn10 = new Isbn10(1234567890);

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

		BookStateRecord bookRecord = library.GetBookRecord(book);

		bookRecord.Should().NotBeNull();
		bookRecord.Book.Should().Be(book);
		bookRecord.State.Should().Be(BookState.InPossession);

		bookRecord.Metadata["onloanto"].Should().Be((user.Id.ToString(), nameof(Guid)));
	}

	[Fact]
	public void Given_a_user_has_a_book_on_loan_when_they_try_to_borrow_again_then_there_is_no_change_in_book_state_or_record()
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
		var firstBookRecords = library.GetBookRecord(book);

		library.LoanBook(book, user);

		var secondBookState = library.GetBookState(book);
		var secondBookRecords = library.GetBookRecord(book);

		secondBookState.Should().Be(firstBookState);
		secondBookRecords.Should().BeEquivalentTo(firstBookRecords);
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
		var firstBookRecords = library.GetBookRecord(book);

		library.LoanBook(book, userAlice);

		var secondBookState = library.GetBookState(book);
		var secondBookRecords = library.GetBookRecord(book);

		secondBookState.Should().Be(firstBookState);
		secondBookRecords.Should().BeEquivalentTo(firstBookRecords);
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
