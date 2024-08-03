using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Exceptions;

namespace HomeBookeper.Domain.Entities;

public class Author : BaseEntity
{
	public Author(string firstName, string lastName)
	{
		if (string.IsNullOrWhiteSpace(firstName))
		{
			throw new InvalidAuthorException($"An author must have a {nameof(firstName)}");
		}

		if (string.IsNullOrWhiteSpace(lastName))
		{
			throw new InvalidAuthorException($"An author must have a {nameof(lastName)}");
		}

		FirstName = firstName;
		LastName = lastName;
	}

	public string FirstName { get; init; }

	public string LastName { get; init; }

	public ICollection<LibraryBook>? Books { get; private set; }
}
