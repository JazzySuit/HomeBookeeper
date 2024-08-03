namespace HomeBookeper.Domain.Exceptions;

public class InvalidAuthorException : Exception
{
	public InvalidAuthorException(string message)
		: base(message)
	{
		
	}
}
