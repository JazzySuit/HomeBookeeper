using AutoMapper;
using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Application.Wrappers;
using HomeBookeper.Domain.Entities;
using MediatR;

namespace HomeBookeper.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommand : IRequest<Response<int>>
{
	public string Title { get; set; }

	public string AuthorFirstName { get; set; }

	public string AuthorLastname { get; set; }

	// multiple authors?

	public string BookType { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<int>>
{
	private readonly IBookRepositoryAsync _bookRepository;
	private readonly IMapper _mapper;

	public CreateBookCommandHandler(IBookRepositoryAsync bookRepository, IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<Response<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
	{
		var book = _mapper.Map<Book>(request);
		//await _bookRepository.AddAsync(book);
		return new Response<int>(0); // bool.Id);
	}
}
