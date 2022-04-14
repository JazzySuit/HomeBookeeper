using HomeBookeper.Domain.Entities;

namespace HomeBookeper.Application.Interfaces.Repositories;

public interface IBookRepositoryAsync : IGenericRepositoryAsync<Book>
{
	Task<bool> IsUniqueIsbnAsync(int isbn);
}
