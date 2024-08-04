using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Extensions;

public static class DomainValueExtensions
{
	public static SearchTitle AsSearchable(this Title title)
		=> new(title.Value);

	public static SearchAuthor AsSearchAble(this Author author)
		=> new(author.FirstName, author.LastName);
}
