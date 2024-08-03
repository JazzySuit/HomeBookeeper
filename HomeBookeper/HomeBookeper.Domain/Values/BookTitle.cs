using HomeBookeper.Domain.Exceptions;
using HomeBookeper.Domain.Extensions;

namespace HomeBookeper.Domain.Values;

public abstract record RecordWithValidation
{
	protected RecordWithValidation() 
	{
		Validate();
	}

	protected virtual void Validate() { }
}

public record BookTitle(string Value) : RecordWithValidation
{
	protected override void Validate()
	{ 
		if (Value.IsNullOrWhiteSpace())
			throw new InvalidTitleException($"The book title {Value} is invalid");
	}
}
