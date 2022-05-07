namespace HomeBookeper.Application.Filters;

public class RequestParameter
{
	public int PageNumber { get; init; }
	public int PageSize { get; init; }

	public RequestParameter()
	{
		PageNumber = 1;
		PageSize = 10;
	}
	public RequestParameter(int pageNumber, int pageSize)
	{
		PageNumber = pageNumber < 1 ? 1 : pageNumber;
		PageSize = pageSize > 10 ? 10 : pageSize;
	}
}
