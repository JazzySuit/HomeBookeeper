using FluentAssertions;
using HomeBookeper.Domain.Entities;
using Xunit;

namespace HomeBookeper.Domain.UnitTests;

public class BookCreationTests
{
	[Fact]
	public void Can_create_a_valid_book_author()
	{
		const string firstName = "book";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		author.Validate().IsValid.Should().BeTrue();
		author.FirstName.Should().Be(firstName);
		author.LastName.Should().Be(lastName);
	}

	[Fact]
	public void An_invalid_book_author_fails_validation()
	{
		var invalidAuthor = GenerateAnInvalidAuthor();

		invalidAuthor.Validate().IsValid.Should().BeFalse();
	}

	[Fact]
	public void Can_create_a_valid_childrens_book()
	{
		const string title = "kids book";
		
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var kidsBook = new ChildrensBook(title, author);

		kidsBook.Validate().IsValid.Should().BeTrue();
		kidsBook.Title.Should().Be(title);
		kidsBook.Author.Should().Be(author);
	}

	[Fact]
	public void Can_create_a_valid_board_book()
	{
		const string title = "kids book";
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var boardBook = new BoardBook(title, author);

		boardBook.Validate().IsValid.Should().BeTrue();
		boardBook.Title.Should().Be(title);
		boardBook.Author.Should().Be(author);
	}

	[Fact]
	public void Can_create_a_valid_fiction_book()
	{
		const string title = "kids book";
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var fictionBook = new FictionBook(title, author);

		fictionBook.Validate().IsValid.Should().BeTrue();
		fictionBook.Title.Should().Be(title);
		fictionBook.Author.Should().Be(author);
	}

	[Fact]
	public void Can_create_a_valid_nonfiction_book()
	{
		const string title = "kids book";
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var nonFictionBook = new NonFictionBook(title, author);

		nonFictionBook.Validate().IsValid.Should().BeTrue();
		nonFictionBook.Title.Should().Be(title);
		nonFictionBook.Author.Should().Be(author);
	}

	[Fact]
	public void A_childrens_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var emptyBookTitle = new ChildrensBook(title: emptyTitle, author: author);

		emptyBookTitle.Validate().IsValid.Should().BeFalse();
		emptyBookTitle.Title.Should().Be(emptyTitle);
		emptyBookTitle.Author.Should().Be(author);
	}

	[Fact]
	public void A_childrens_book_must_have_an_author_validation_fails_when_an_invalid_author_is_given()
	{
		const string title = "Title";
		var invalidAuthor = GenerateAnInvalidAuthor();

		var emptyBookAuthor = new ChildrensBook(title: title, author: invalidAuthor);

		emptyBookAuthor.Validate().IsValid.Should().BeFalse();
		emptyBookAuthor.Title.Should().Be(title);
	}

	[Fact]
	public void A_board_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var emptyBookTitle = new BoardBook(title: emptyTitle, author: author);

		emptyBookTitle.Validate().IsValid.Should().BeFalse();
		emptyBookTitle.Title.Should().Be(emptyTitle);
		emptyBookTitle.Author.Should().Be(author);
	}

	[Fact]
	public void A_board_book_must_have_an_author_validation_fails_when_an_invalid_author_is_given()
	{
		const string title = "Title";
		var invalidAuthor = GenerateAnInvalidAuthor();

		var emptyBookAuthor = new BoardBook(title: title, author: invalidAuthor);

		emptyBookAuthor.Validate().IsValid.Should().BeFalse();
		emptyBookAuthor.Title.Should().Be(title);
	}

	[Fact]
	public void A_fiction_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var emptyBookTitle = new FictionBook(title: emptyTitle, author: author);

		emptyBookTitle.Validate().IsValid.Should().BeFalse();
		emptyBookTitle.Title.Should().Be(emptyTitle);
		emptyBookTitle.Author.Should().Be(author);
	}

	[Fact]
	public void A_fiction_book_must_have_an_author_validation_fails_when_an_invalid_author_is_given()
	{
		const string title = "Title";
		var invalidAuthor = GenerateAnInvalidAuthor();

		var emptyBookAuthor = new FictionBook(title: title, author: invalidAuthor);

		emptyBookAuthor.Validate().IsValid.Should().BeFalse();
		emptyBookAuthor.Title.Should().Be(title);
	}

	[Fact]
	public void A_nonfiction_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		const string firstName = "kids";
		const string lastName = "author";

		var author = new Author(firstName, lastName);

		var emptyBookTitle = new NonFictionBook(title: emptyTitle, author: author);

		emptyBookTitle.Validate().IsValid.Should().BeFalse();
		emptyBookTitle.Title.Should().Be(emptyTitle);
		emptyBookTitle.Author.Should().Be(author);
	}

	[Fact]
	public void A_nonfiction_book_must_have_an_author_validation_fails_when_an_invalid_author_is_given()
	{
		const string title = "Title";
		var invalidAuthor = GenerateAnInvalidAuthor();

		var emptyBookAuthor = new NonFictionBook(title: title, author: invalidAuthor);

		emptyBookAuthor.Validate().IsValid.Should().BeFalse();
		emptyBookAuthor.Title.Should().Be(title);
		emptyBookAuthor.Author.Should().Be(invalidAuthor);
	}

	private Author GenerateAnInvalidAuthor()
		=> new Author(string.Empty, string.Empty);
}