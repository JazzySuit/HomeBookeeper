using HomeBookeper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeBookeper.Infrastructure.Persistence.Configurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.Property(author => author.FirstName)
			.HasMaxLength(256)
			.IsRequired();

		builder.Property(author => author.LastName)
			.HasMaxLength(256)
			.IsRequired();

		//builder.HasMany(author => author.Books)
		//	.WithMany(book => book.Authors);

		//builder.Property(author => author.Books)
		//	.IsRequired();
	}
}
