namespace HomeBookeper.Domain.Exceptions;

public class InvalidIsbnException : Exception
{
	public InvalidIsbnException(string message)
		: base(message)
	{

	}
}
