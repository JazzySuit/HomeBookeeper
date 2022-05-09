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
	public void Attempting_to_create_an_invalid_isbn_throws_an_exception()
	{
		Assert.Throws<ArgumentException>(() => new Isbn(IsbnStandard.Isbn10, 123456));
	}

	[Fact]
	public void Can_create_a_valid_childrens_book()
	{
		const string title = "kids book";
		const string publisher = "only kids books";
		var author = GenerateSingleValidAuthorEnumerable().First();
		var isbn13 = GenerateIsbn13();

		var kidsBookNoAuthor = new Book(
			title,
			BookType.ChildrensBook,
			isbn13,
			publisher);

		kidsBookNoAuthor.Title.Should().Be(title);
		kidsBookNoAuthor.Authors.Count.Should().Be(0);
		kidsBookNoAuthor.Isbn.Should().Be(isbn13);

		//var kidsBookWithAuthor = new Book(
		//	title,
		//	BookType.ChildrensBook,
		//	isbn13,
		//	publisher,
		//	author);

		//kidsBookWithAuthor.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void Can_create_a_valid_board_book()
	{
		const string title = "kids book";
		const string publisher = "book publisher";
		var author = GenerateSingleValidAuthorEnumerable().First();
		var isbn13 = GenerateIsbn13();

		var boardBookNoAuthor = new Book(
			title,
			BookType.BoardBook,
			isbn13,
			publisher);

		boardBookNoAuthor.Title.Should().Be(title);
		boardBookNoAuthor.Authors.Count.Should().Be(0);
		boardBookNoAuthor.Isbn.Should().Be(isbn13);

		//var boardBookWithAuthor = new Book(
		//	title,
		//	BookType.ChildrensBook,
		//	isbn13,
		//	publisher,
		//	author);

		//boardBookWithAuthor.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void Can_create_a_valid_fiction_book()
	{
		const string title = "kids book";
		const string publisher = "book publisher";
		var author = GenerateSingleValidAuthorEnumerable().First();
		var isbn13 = GenerateIsbn13();

		var fictionBookNoAuthor = new Book(
			title,
			BookType.FictionBook,
			isbn13,
			publisher);

		fictionBookNoAuthor.Title.Should().Be(title);
		fictionBookNoAuthor.Authors.Count.Should().Be(0);

		//var fictionBookWithAuthor = new Book(
		//	title,
		//	BookType.ChildrensBook,
		//	isbn13,
		//	publisher,
		//	author);

		//fictionBookWithAuthor.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void Can_create_a_valid_nonfiction_book()
	{
		const string title = "kids book";
		const string publisher = "book publisher";
		var author = GenerateSingleValidAuthorEnumerable().First();
		var isbn13 = GenerateIsbn13();

		var nonFictionBook = new Book(
			title,
			BookType.NonFictionBook,
			isbn13,
			publisher);

		nonFictionBook.Title.Should().Be(title);
		nonFictionBook.Authors.Count.Should().Be(0);

		//var nonFictionBookWithAuthor = new Book(
		//	title,
		//	BookType.ChildrensBook,
		//	isbn13,
		//	publisher,
		//	author);

		//nonFictionBookWithAuthor.Authors.Count.Should().Be(1);
	}

	[Fact]
	public void Throw_an_exception_on_initialisation_when_a_book_has_no_title()
	{
		const string emptyTitle = "";
		const string publisher = "book publisher";
		var isbn13 = GenerateIsbn13();

		Assert.Throws<ArgumentException>(() => new Book(
			emptyTitle,
			BookType.ChildrensBook,
			isbn13,
			publisher));
	}

	[Fact]
	public void Throw_an_exception_on_initialisation_when_a_book_has_no_isbn()
	{
		const string title = "book title";
		const string publisher = "book publisher";
		Isbn isbn13 = null;

		Assert.Throws<ArgumentNullException>(() => new Book(
			title,
			BookType.ChildrensBook,
			isbn13,
			publisher));
	}

	#region private helper test methods

	private Author GenerateValidAuthor()
		=> new("Bobs", "Anauthor");

	private IEnumerable<Author> GenerateSingleValidAuthorEnumerable()
	{
		yield return GenerateValidAuthor();
	}

	private Isbn GenerateIsbn13()
		=> new Isbn(
			IsbnStandard.Isbn13,
			1234567899123
		);
	#endregion
}