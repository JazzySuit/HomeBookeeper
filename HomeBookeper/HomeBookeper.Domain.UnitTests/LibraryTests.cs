using FluentAssertions;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Interfaces;
using HomeBookeper.Domain.Values;
using Xunit;

namespace HomeBookeper.Domain.UnitTests;

public class LibraryTests
{
	// Book state "diagram"
	// ## record cannot be deleted
	// book states: (+wishlist)->(+in possesion)<->(on loan, needs repair)->(sold, given away, destroyed)


	[Fact]
	public void Can_add_a_new_valid_book_to_the_library()
	{
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		var addBookToLibrary = () => library.AddNewBook(book, user);

		addBookToLibrary.Should().NotThrow();

		var addedBook = library.FindBook(isbn10);

		addedBook.Should().NotBeNull();
		addedBook.Should().Be(book);
	}

	[Fact]
	public void Adding_a_book_to_the_library_that_already_exists_does_not_get_added_again()
	{
		// TODO: future feature - to allow adding multiple of the same books
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		library.AddNewBook(book, user);

		var addBookToLibrary = () => library.AddNewBook(book, user);

		addBookToLibrary.Should().NotThrow();

		var addedBook = library.FindBook(isbn10);

		addedBook.Should().Be(book);
	}

	[Fact]
	public void Adding_a_new_book_to_the_library_has_a_book_state_as_in_possession()
	{
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		library.AddNewBook(book, user);

		var bookState = library.GetBookState(book);

		bookState.Should().Be(BookState.InPossession);
	}

	[Fact]
	public void Wishlisting_a_book_into_the_library_creates_a_new_book_object()
	{
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		library.WishlistBook(book, user);

		var addedBook = library.FindBook(isbn10);

		addedBook.Should().NotBeNull();
		addedBook.Should().Be(book);
	}

	[Fact]
	public void Wishlisting_a_book_into_the_library_has_a_book_state_as_wishlist()
	{
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		library.WishlistBook(book, user);

		var bookState = library.GetBookState(book);

		bookState.Should().Be(BookState.OnWishlist);
	}

	[Fact]
	public void Wishlisting_a_book_that_already_exists_in_the_library_does_not_add_the_book_as_wishlisted()
	{
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		library.AddNewBook(book, user);

		var wishlistAction = () => library.WishlistBook(book, user);

		wishlistAction.Should().NotThrow();

		var bookState = library.GetBookState(book);

		bookState.Should().Be(BookState.InPossession);
	}

	[Fact]
	public void Adding_a_book_that_has_been_wishlisted_adds_the_book_to_the_library_as_in_possession()
	{
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);
		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibrary library = new Library();

		library.WishlistBook(book, user);
		
		library.AddNewBook(book, user);

		var bookState = library.GetBookState(book);

		bookState.Should().Be(BookState.InPossession);
	}

	[Fact]
	public void Loaning_out_a_book_updates_the_state_of_a_book_to_onloan_and_who_it_is_loaned_to()
	{
		// is the book lent out & to who?
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);

		IBook book = new Book(
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
		bookRecord.State.Should().Be(BookState.OnLoan);

		bookRecord.Metadata["onloanto"].Should().Be((user.Id.ToString(), nameof(Guid)));
	}

	[Fact]
	public void Cannot_loan_out_a_book_that_is_already_on_loan()
	{
		var isbn10 = new Isbn(IsbnStandard.Isbn10, 1234567890);

		IBook book = new Book(
			"Book Title",
			new Author("Bobb", "Bearly"),
			BookType.FictionBook,
			isbn10,
			"Book Publisher");

		ILibraryUser user = new LibraryUser("Joe", "Bloggs");

		ILibrary library = new Library();

		library.AddNewBook(book, user);

		library.LoanBook(book, user);

		var secondLoanAction = () => library.LoanBook(book, user);
		secondLoanAction.Should().Throw<InvalidOperationException>();
	}

	[Fact]
	public void Returning_a_book_that_is_on_loan_sets_the_books_state_back_to_in_possession()
	{
		Assert.False(true);
	}

	[Fact]
	public void A_book_that_needs_to_be_repaired_has_its_state_set_to_needs_repair()
	{
		Assert.False(true);
	}

	[Fact]
	public void When_a_book_is_repaired_the_books_state_is_set_to_in_possession()
	{
		Assert.False(true);
	}

	[Fact]
	public void Selling_a_book_sets_a_books_state_to_sold()
	{
		Assert.False(true);
	}

	[Fact]
	public void A_book_that_has_been_sold_cannot_be_set_back_to_any_other_book_state()
	{
		Assert.False(true);
	}

	[Fact]
	public void Giving_away_a_book_sets_a_books_state_to_given_away()
	{
		Assert.False(true);
	}

	[Fact]
	public void A_book_that_has_been_given_away_cannot_be_set_back_to_any_other_book_state()
	{
		Assert.False(true);
	}

	[Fact]
	public void Destroying_a_book_sets_a_books_state_to_destroyed()
	{
		Assert.False(true);
	}

	[Fact]
	public void A_book_that_has_been_destroyed_cannot_be_set_back_to_any_other_book_state()
	{
		Assert.False(true);
	}
}
