using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Infrastructure.Persistence.Contexts;
using HomeBookeper.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace HomeBookeper.Infrastructure.Persistence.Repositories;

public class BookRepositoryAsync : GenericRepositoryAsync<Book>, IBookRepositoryAsync
{
	private readonly DbSet<Book> _books;

	public BookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
	{
		_books = dbContext.Set<Book>();
	}

	public Task<bool> IsUniqueIsbnAsync(int isbn)
	{
		throw new NotImplementedException();
	}

}
