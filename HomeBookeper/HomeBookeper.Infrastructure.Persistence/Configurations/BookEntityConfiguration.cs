using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeBookeper.Infrastructure.Persistence.Configurations;

public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.HasKey(book => book.Id);

		builder.HasDiscriminator<string>("BookType");
		
		builder.Property(book => book.Title)
			.HasMaxLength(256)
			.IsRequired();

		//builder.HasMany(book => book.Authors)
		//	.WithMany(author => author.Books);

		builder.Property(book => book.Isbn)
			.HasConversion(IsbnConverter())
			.IsRequired();
	}

	private static ValueConverter IsbnConverter() 
		=> new ValueConverter<Isbn, long>(
			isbn => isbn.Value,
			longValue => CreateIsbnFromDbValue(longValue));

	private static Isbn CreateIsbnFromDbValue(long value)
		=> value.ToString().Length == 13
				? new Isbn() { Value = value, Standard = IsbnStandard.Isbn13 }
				: new Isbn() { Value = value, Standard = IsbnStandard.Isbn10 };
}
