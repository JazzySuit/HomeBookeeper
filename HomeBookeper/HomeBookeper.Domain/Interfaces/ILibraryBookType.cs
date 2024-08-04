using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Interfaces;

public interface ILibraryBookType 
{
	public Isbn Isbn { get; }
	public Title Title { get; }

	public IReadOnlyCollection<Author> Authors { get; }
}

