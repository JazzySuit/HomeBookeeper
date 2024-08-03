using FluentAssertions;
using HomeBookeper.Domain.Entities.Books;
using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.UnitTests.Fixture;
using HomeBookeper.Domain.Values;
using Xunit;

namespace HomeBookeper.Domain.UnitTests;

public class NewBookTests :  IClassFixture<BookTestFixture>
{
	private readonly BookTestFixture _fixture;

	public NewBookTests(BookTestFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact]
	public void Given_a_book_title_when_creating_a_title_then_there_is_a_validated_digital_instance()
	{
		// happy path
		var createTitle = () => new BookTitle("testing book titles");

		createTitle.Should().NotThrow();

		var newTitle = createTitle();

		newTitle.Value.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public void Given_an_empty_book_title_when_creating_a_title_then_validation_fails()
	{
		var createTitle = () => new BookTitle(string.Empty);

		createTitle.Should().Throw<InvalidTitleException>();
	}

	[Fact]
	public void Given_only_whitespace_for_a_book_title_when_creating_a_title_then_validation_fails()
	{
		var createTitle = () => new BookTitle(" \t\n\r");

		createTitle.Should().Throw<InvalidTitleException>();
	}

	[Fact]
	public void Given_no_book_title_when_creating_a_title_then_validation_fails()
	{
		var createTitle = () => new BookTitle(null);

		createTitle.Should().Throw<InvalidTitleException>();
	}

	[Fact]
	public void Given_book_details_when_creating_a_new_book_then_there_is_a_digital_instance()
	{
		// happy path
		var createNewBookA = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			new BookTitle("The book of all books"),
			_fixture.ValidAuthor);

		var createNewBookB = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			"The book of all books",
			_fixture.ValidAuthor);

		var createNewBookC = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			"The book of all books",
			"Jane", "Doe");

		createNewBookA.Should().NotThrow();
		createNewBookB.Should().NotThrow();
		createNewBookC.Should().NotThrow();

		var bookA = createNewBookA();
		var bookB = createNewBookB();
		var bookC = createNewBookC();

		bookA.Isbn.Should().NotBeNull();
		bookA.Authors.Should().NotBeNull();
		bookA.Authors.Count.Should().Be(1);
		bookA.Title.Should().NotBeNull();

		bookA.Should().BeEquivalentTo(bookB);
		bookB.Should().BeEquivalentTo(bookC);
	}
	
	[Fact]
	public void Given_no_book_isbn_when_creating_a_new_book_then_fail_to_create_a_digital_instance()
	{
		var createNewBook = () => NewBook.Create(null,
			"The book of all books",
			_fixture.ValidAuthor);

		createNewBook.Should().Throw<InvalidBookException>();
	}

	[Fact]
	public void Given_no_book_title_when_creating_a_new_book_then_fail_to_create_a_digital_instance()
	{
		BookTitle title = null;
		var createNewBook = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			title,
			_fixture.ValidAuthor);

		createNewBook.Should().Throw<InvalidBookException>();
	}

	[Fact]
	public void Given_an_empty_book_title_when_creating_a_new_book_then_fail_to_create_a_digital_instance()
	{
		var createNewBook = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			"",
			_fixture.ValidAuthor);

		createNewBook.Should().Throw<InvalidTitleException>();
	}


	[Fact]
	public void Given_no_book_author_when_creating_a_new_book_then_fail_to_create_a_digital_instance()
	{
		var createNewBook = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			"The book of all books",
			null);

		createNewBook.Should().Throw<InvalidBookException>();
	}

	[Fact]
	public void Given_an_empty_author_first_name_when_creating_a_new_book_then_fail_to_create_a_digital_instance()
	{
		var createNewBook = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			"The book of all books",
			string.Empty, "sdf");

		createNewBook.Should().Throw<InvalidAuthorException>();
	}

	[Fact]
	public void Given_an_empty_author_last_name_when_creating_a_new_book_then_fail_to_create_a_digital_instance()
	{
		var createNewBook = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			"The book of all books",
			 "sdf", string.Empty);

		createNewBook.Should().Throw<InvalidAuthorException>();
	}

	[Fact]
	public void Given_no_book_author_names_when_creating_a_new_book_then_fail_to_create_a_digital_instance()
	{
		var createNewBook = () => NewBook.Create(_fixture.GenerateValidIsbn10(),
			"The book of all books",
			null, null);

		createNewBook.Should().Throw<InvalidAuthorException>();
	}
}
