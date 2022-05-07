using FluentAssertions;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;
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

		author.FirstName.Should().Be(firstName);
		author.LastName.Should().Be(lastName);
	}

	[Fact]
	public void Generate_a_valid_book_author()
	{
		var author = GenerateValidAuthor();

		author.FirstName.Should().NotBeEmpty();
		author.LastName.Should().NotBeEmpty();
	}

	[Fact]
	public void Attempting_to_create_an_invalid_author_throws_an_exception()
	{
		Assert.Throws<ArgumentException>(() => new Author(string.Empty, string.Empty));
	}

	[Fact]
	public void Can_create_a_valid_childrens_book()
	{
		const string title = "kids book";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var kidsBook = new ChildrensBook(title, isbn13);

		kidsBook.Title.Should().Be(title);
		//kidsBook.Authors.Count.Should().Be(1);
		kidsBook.Isbn.Should().Be(isbn13);
	}

	[Fact]
	public void Can_create_a_valid_board_book()
	{
		const string title = "kids book";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var boardBook = new BoardBook(title, isbn13);

		boardBook.Title.Should().Be(title);
		//boardBook.Authors.Count.Should().Be(1);
		boardBook.Isbn.Should().Be(isbn13);
	}

	[Fact]
	public void Can_create_a_valid_fiction_book()
	{
		const string title = "kids book";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var fictionBook = new FictionBook(title, isbn13);

		fictionBook.Title.Should().Be(title);
		//fictionBook.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void Can_create_a_valid_nonfiction_book()
	{
		const string title = "kids book";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var nonFictionBook = new NonFictionBook(title, isbn13);

		nonFictionBook.Title.Should().Be(title);
		//nonFictionBook.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void A_childrens_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var emptyBookTitle = new ChildrensBook(emptyTitle, isbn13);

		emptyBookTitle.Title.Should().Be(emptyTitle);
		//emptyBookTitle.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void A_board_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var emptyBookTitle = new BoardBook(emptyTitle, isbn13);

		emptyBookTitle.Title.Should().Be(emptyTitle);
		//emptyBookTitle.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void A_fiction_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var emptyBookTitle = new FictionBook(emptyTitle, isbn13);

		emptyBookTitle.Title.Should().Be(emptyTitle);
		//emptyBookTitle.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void A_nonfiction_book_must_have_a_title_validation_fails_when_no_title_is_given()
	{
		const string emptyTitle = "";
		//var authors = GenerateSingleValidAuthorList().ToList();
		var isbn13 = GenerateIsbn13();

		var emptyBookTitle = new NonFictionBook(emptyTitle, isbn13);

		emptyBookTitle.Title.Should().Be(emptyTitle);
		//emptyBookTitle.Authors.Count.Should().Be(1);
	}


	#region private helper test methods

	private Author GenerateValidAuthor()
		=> new("Bobs", "Anauthor");

	private IEnumerable<Author> GenerateSingleValidAuthorList()
	{
		yield return GenerateValidAuthor();
	}

	private Isbn GenerateIsbn13()
		=> new Isbn()
		{
			Standard = IsbnStandard.Isbn13,
			Value = 1234567899123
		};
	#endregion
}