using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;

namespace HomeBookeper.Domain.Extensions;

public static class DomainValueExtensions
{
	public static Title AsSearchable(this BookTitle title)
		=> new(title.Value);

	public static AuthorName AsSearchAble(this Author author)
		=> new(author.FirstName, author.LastName);
}
