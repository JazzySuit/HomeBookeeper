using FluentValidation;
using HomeBookeper.Application.Interfaces.Repositories;

namespace HomeBookeper.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
	private readonly IBookRepositoryAsync _bookRepository;

	public CreateBookCommandValidator(IBookRepositoryAsync bookRepository)
	{
		_bookRepository = bookRepository;

		// Rules
	}

	private async Task<bool> IsIsbnUniqueAsync(int isbnValue, CancellationToken cancellationToken) 
		=> await _bookRepository.IsUniqueIsbnAsync(isbnValue);
}
