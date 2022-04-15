using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Infrastructure.Persistence.Contexts;
using HomeBookeper.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeBookeper.Infrastructure.Persistence.Repositories;

public class BookRepositoryAsync : GenericRepositoryAsync<Book>, IBookRepositoryAsync
{
	private readonly DbSet<BoardBook> _boardBooks;
	private readonly DbSet<ChildrensBook> _childrensBooks;
	private readonly DbSet<FictionBook> _fictionBooks;
	private readonly DbSet<NonFictionBook> _nonFictionBooks;

	public BookRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
	{
		_boardBooks = dbContext.Set<BoardBook>();
		_childrensBooks = dbContext.Set<ChildrensBook>();
		_fictionBooks = dbContext.Set<FictionBook>();
		_nonFictionBooks = dbContext.Set<NonFictionBook>();
	}

	#region add book types

	public Task<BoardBook> AddAsync(BoardBook entity)
		=> AddAsync(entity);

	public Task<ChildrensBook> AddAsync(ChildrensBook entity)
		=> AddAsync(entity);

	public Task<FictionBook> AddAsync(FictionBook entity)
		=> AddAsync(entity);

	public Task<NonFictionBook> AddAsync(NonFictionBook entity)
		=> AddAsync(entity);

	#endregion

	#region updates

	public Task UpdateAsync(BoardBook entity)
	{
		throw new System.NotImplementedException();
	}

	public Task UpdateAsync(ChildrensBook entity)
	{
		throw new System.NotImplementedException();
	}

	public Task UpdateAsync(FictionBook entity)
	{
		throw new System.NotImplementedException();
	}

	public Task UpdateAsync(NonFictionBook entity)
	{
		throw new System.NotImplementedException();
	}

	#endregion

	public Task<bool> IsUniqueIsbnAsync(int isbn)
	{
		throw new System.NotImplementedException();
	}

}
