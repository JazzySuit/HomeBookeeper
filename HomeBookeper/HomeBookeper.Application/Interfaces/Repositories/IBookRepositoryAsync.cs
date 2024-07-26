using HomeBookeper.Domain.Entities;

namespace HomeBookeper.Application.Interfaces.Repositories;

public interface IBookRepositoryAsync : IGenericRepositoryAsync<LibraryBook>
{
	Task<bool> IsUniqueIsbnAsync(int isbn);
}
