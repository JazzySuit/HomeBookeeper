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

		builder.Property(book => book.Type)
			.HasColumnName(nameof(BookType))
			.HasConversion(
				bookType => bookType.ToString(),
				dbString => (BookType)Enum.Parse(typeof(BookType), dbString))
			.IsRequired();
		
		builder.Property(book => book.Title)
			.HasMaxLength(256)
			.IsRequired();

		builder.Property(book => book.Publisher)
			.HasMaxLength(256)
			.IsRequired();

		builder.Property(book => book.Series)
			.HasMaxLength(256);

		builder.HasMany(book => book.Authors)
			.WithMany(author => author.Books);

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
				? new Isbn(IsbnStandard.Isbn13, value)
				: new Isbn(IsbnStandard.Isbn10, value);
}
