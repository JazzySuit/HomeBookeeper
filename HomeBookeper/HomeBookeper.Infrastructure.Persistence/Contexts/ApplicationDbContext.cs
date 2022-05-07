using HomeBookeper.Application.Interfaces;
using HomeBookeper.Domain.Common;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HomeBookeper.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
	private readonly IDateTimeService _dateTime;
	private readonly IAuthenticatedUserService _authenticatedUser;

	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options, 
		IDateTimeService dateTime, 
		IAuthenticatedUserService authenticatedUser) 
		: base(options)
	{
		//ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		_dateTime = dateTime;
		_authenticatedUser = authenticatedUser;
	}



	public DbSet<Product> Products { get; set; }

	public DbSet<BoardBook> BoardBooks { get; set; }
	public DbSet<ChildrensBook> ChildrensBooks { get; set; }
	public DbSet<FictionBook> FictionBooks { get; set; }
	public DbSet<NonFictionBook> NonFictionBooks { get; set; }

	public DbSet<Author> Authors { get; set; }



	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.Created = _dateTime.NowUtc;
					entry.Entity.CreatedBy = _authenticatedUser.UserId;
					break;
				case EntityState.Modified:
					entry.Entity.LastModified = _dateTime.NowUtc;
					entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
					break;
			}
		}
		return base.SaveChangesAsync(cancellationToken);
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		//All Decimals will have 18,6 Range
		foreach (var decimalProperty in builder.Model.GetEntityTypes()
			.SelectMany(t => t.GetProperties())
			.Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
		{
			decimalProperty.SetColumnType("decimal(18,6)");
		}

		builder.ApplyConfiguration(new BookEntityConfiguration());

		base.OnModelCreating(builder);
	}
}
