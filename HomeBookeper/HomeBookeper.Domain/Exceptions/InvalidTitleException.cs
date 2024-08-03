namespace HomeBookeper.Domain.Exceptions;

public class InvalidTitleException : Exception
{
	public InvalidTitleException(string message)
		:base(message)
	{
		
	}
}
