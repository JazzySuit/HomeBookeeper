using AutoMapper;
using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Application.Wrappers;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Domain.Values;
using MediatR;

namespace HomeBookeper.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommand : IRequest<Response<int>>
{
	public string Title { get; set; }

	public Isbn Isbn { get; set; }

	public BookType BookType { get; set; }
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
		var book = MapToBookType(request);
		await _bookRepository.AddAsync(book);
		
		return new Response<int>(book.Id);
	}

	private Book MapToBookType(CreateBookCommand request)
		=> request.BookType switch
		{
			BookType.BoardBook => _mapper.Map<BoardBook>(request),
			BookType.ChildrensBook => _mapper.Map<ChildrensBook>(request),
			BookType.FictionBook => _mapper.Map<FictionBook>(request),
			BookType.NonFictionBook => _mapper.Map<NonFictionBook>(request),
			_ => throw new NotImplementedException()
		};
}
