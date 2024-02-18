using HomeBookeper.Domain.Interfaces;

namespace HomeBookeper.Domain.Entities;

public class LibraryUser : ILibraryUser
{
	private readonly string _firstName;
	private readonly string _secondName;

	public LibraryUser(string firstName, string secondName)
	{
		Id = Guid.NewGuid();
		_firstName = firstName;
		_secondName = secondName;
	}

	public Guid Id { get; init; }

	public string Name => $"{_firstName} {_secondName}";
}