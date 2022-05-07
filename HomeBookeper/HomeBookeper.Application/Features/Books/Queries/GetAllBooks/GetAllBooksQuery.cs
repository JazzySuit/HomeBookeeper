using AutoMapper;
using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Application.Wrappers;
using MediatR;

namespace HomeBookeper.Application.Features.Books.Queries.GetAllBooks;

public class GetAllBooksQuery : IRequest<PagedResponse<IEnumerable<GetAllBooksViewModel>>>
{
	public int PageNumber { get; init; }
	public int PageSize { get; init; }
}

public class GetAllBooksQueryHandler
	: IRequestHandler<GetAllBooksQuery,
		PagedResponse<IEnumerable<GetAllBooksViewModel>>>
{
	private readonly IBookRepositoryAsync _bookRepository;
	private readonly IMapper _mapper;

	public GetAllBooksQueryHandler(IBookRepositoryAsync bookRepository, IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<PagedResponse<IEnumerable<GetAllBooksViewModel>>> Handle(
		GetAllBooksQuery request,
		CancellationToken cancellationToken)
	{
		var validFilter = _mapper.Map<GetAllBooksParameter>(request);
		var book = await _bookRepository.GetPagedReponseAsync(
			validFilter.PageNumber,
			validFilter.PageSize);

		var bookViewModel = _mapper.Map<IEnumerable<GetAllBooksViewModel>>(book);

		return new PagedResponse<IEnumerable<GetAllBooksViewModel>>(
			bookViewModel,
			validFilter.PageNumber,
			validFilter.PageSize);
	}
}
