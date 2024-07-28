using HomeBookeper.Domain.Interfaces;

namespace HomeBookeper.Domain.Common;

public abstract class Transaction<T>
{
	public Transaction(T transactionValue, ILibraryUser actioningUser)
	{
		Id = Guid.NewGuid();
		ActionedOn = DateTime.Now;
		ActionedBy = actioningUser.Name;
		Value = transactionValue;
	}

	public Guid Id { get; init; }

	public DateTime ActionedOn { get; private set; }

	public string ActionedBy { get; private set; }

	public T Value { get; private set; }
}
