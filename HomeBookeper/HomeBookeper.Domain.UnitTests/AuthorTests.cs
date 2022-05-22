using FluentAssertions;
using HomeBookeper.Domain.Entities;
using Xunit;

namespace HomeBookeper.Domain.UnitTests;

public class AuthorTests
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


	#region private helper test methods

	private Author GenerateValidAuthor()
		=> new("Bobs", "Anauthor");

	private IEnumerable<Author> GenerateSingleValidAuthorEnumerable()
	{
		yield return GenerateValidAuthor();
	}

	#endregion
}
