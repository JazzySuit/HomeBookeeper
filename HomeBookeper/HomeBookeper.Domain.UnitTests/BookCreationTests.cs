using FluentAssertions;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.UnitTests.Fixture;
using HomeBookeper.Domain.Values;
using Xunit;

namespace HomeBookeper.Domain.UnitTests;

public class BookCreationTests : IClassFixture<BookTestFixture>
{
	private readonly BookTestFixture _bookFixture;

	public BookCreationTests(BookTestFixture bookTestFixtures)
	{
		_bookFixture = bookTestFixtures;
	}

	[Fact]
	public void Attempting_to_create_an_invalid_isbn_throws_an_exception()
	{
		Assert.Throws<InvalidIsbnException>(() => new Isbn(IsbnStandard.Isbn10, 123456));
	}

	[Fact]
	public void Can_create_a_childrens_book()
	{
		var isbn13 = _bookFixture.GenerateValidIsbn();

		var kidsBookNoAuthor = new Book(
			_bookFixture.ValidBookTitle,
			BookType.ChildrensBook,
			isbn13,
			_bookFixture.ValidBookPublisher);

		kidsBookNoAuthor.Title.Should().Be(_bookFixture.ValidBookTitle);
		kidsBookNoAuthor.Publisher.Should().Be(_bookFixture.ValidBookPublisher);
		kidsBookNoAuthor.Authors.Count.Should().Be(0);
		kidsBookNoAuthor.Isbn.Should().Be(isbn13);
	}

	[Fact]
	public void Can_create_a_board_book()
	{
		var isbn13 = _bookFixture.GenerateValidIsbn();

		var boardBookNoAuthor = new Book(
			_bookFixture.ValidBookTitle,
			BookType.BoardBook,
			isbn13,
			_bookFixture.ValidBookPublisher);

		boardBookNoAuthor.Title.Should().Be(_bookFixture.ValidBookTitle);
		boardBookNoAuthor.Publisher.Should().Be(_bookFixture.ValidBookPublisher);
		boardBookNoAuthor.Authors.Count.Should().Be(0);
		boardBookNoAuthor.Isbn.Should().Be(isbn13);
	}

	[Fact]
	public void Can_create_a_fiction_book()
	{
		var fictionBookNoAuthor = new Book(
			_bookFixture.ValidBookTitle,
			BookType.FictionBook,
			_bookFixture.GenerateValidIsbn(),
			_bookFixture.ValidBookPublisher);

		fictionBookNoAuthor.Title.Should().Be(_bookFixture.ValidBookTitle);
		fictionBookNoAuthor.Publisher.Should().Be(_bookFixture.ValidBookPublisher);
		fictionBookNoAuthor.Authors.Count.Should().Be(0);
	}

	[Fact]
	public void Can_create_a_nonfiction_book()
	{
		var nonFictionBook = new Book(
			_bookFixture.ValidBookTitle,
			BookType.NonFictionBook,
			_bookFixture.GenerateValidIsbn(),
			_bookFixture.ValidBookPublisher);

		nonFictionBook.Title.Should().Be(_bookFixture.ValidBookTitle);
		nonFictionBook.Publisher.Should().Be(_bookFixture.ValidBookPublisher);
		nonFictionBook.Authors.Count.Should().Be(0);
	}

	[Fact]
	public void Throw_an_exception_when_creating_a_book_with_no_title()
	{
		Assert.Throws<InvalidBookException>(() => new Book(
			string.Empty,
			BookType.ChildrensBook,
			_bookFixture.GenerateValidIsbn(),
			_bookFixture.ValidBookPublisher));
	}
	[Fact]
	public void Throw_an_exception_when_creating_a_book_with_no_publisher()
	{
		Assert.Throws<InvalidBookException>(() => new Book(
			_bookFixture.ValidBookTitle,
			BookType.ChildrensBook,
			_bookFixture.GenerateValidIsbn(),
			string.Empty));
	}

	[Fact]
	public void Throw_an_exception_when_creating_a_book_with_no_isbn()
	{
		Assert.Throws<InvalidIsbnException>(() => new Book(
			_bookFixture.ValidBookTitle,
			BookType.ChildrensBook,
			isbn: null,
			_bookFixture.ValidBookPublisher));
	}

	#region private helper test methods



	#endregion
}