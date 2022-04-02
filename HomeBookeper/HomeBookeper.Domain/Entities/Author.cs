using FluentValidation;
using FluentValidation.Results;

namespace HomeBookeper.Domain.Entities;

public class Author
{
	private readonly string _firstName;
	private readonly string _lastName;
	private readonly AuthorValidator _validator;

	public Author(string firstName, string lastName)
	{
		_firstName = firstName;
		_lastName = lastName;

		_validator = new AuthorValidator();
	}

	public string FirstName => _firstName;

	public string LastName => _lastName;

	public ValidationResult Validate() => _validator.Validate(this);
}

internal class AuthorValidator : AbstractValidator<Author>
{
	public AuthorValidator()
	{
		RuleFor(author => author.FirstName).NotEmpty();
		RuleFor(author => author.LastName).NotEmpty();
	}
}
