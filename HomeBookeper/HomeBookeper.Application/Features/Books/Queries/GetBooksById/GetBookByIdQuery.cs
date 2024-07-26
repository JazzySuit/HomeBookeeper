using HomeBookeper.Application.Exceptions;
using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Application.Wrappers;
using HomeBookeper.Domain.Entities;
using MediatR;

namespace HomeBookeper.Application.Features.Books.Queries.GetBooksById;

public class GetBookByIdQuery : IRequest<Response<LibraryBook>>
{
	public int Id { get; set; }

	public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Response<LibraryBook>>
	{
		private readonly IBookRepositoryAsync _bookRepository;

		public GetBookByIdQueryHandler(IBookRepositoryAsync bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<Response<LibraryBook>> Handle(
			GetBookByIdQuery request, 
			CancellationToken cancellationToken)
		{
			var book = await _bookRepository.GetByIdAsync(request.Id);
			if (book == null)
				throw new ApiException("Book not found");

			return new Response<LibraryBook>(book);
		}
	}
}
