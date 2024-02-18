using HomeBookeper.Domain.Enums;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibraryTransaction
{
	public Guid Id { get; }

	public TransactionType Type { get; }

	public DateTime ActionedOn { get; }

	public string ActionedBy { get; }

	Isbn Value { get; }
}