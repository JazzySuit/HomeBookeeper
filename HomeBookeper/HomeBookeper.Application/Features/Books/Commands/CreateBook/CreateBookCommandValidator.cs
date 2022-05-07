using FluentValidation;
using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Domain.Entities;

namespace HomeBookeper.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
	private readonly IBookRepositoryAsync _bookRepository;

	public CreateBookCommandValidator(IBookRepositoryAsync bookRepository)
	{
		_bookRepository = bookRepository;

		// Rules
		RuleFor(b => b.Title)
			.NotNull()
			.NotEmpty().WithMessage("{PropertyName} is required")
			.MaximumLength(256).WithMessage("{PropertyName} must not exceed 256 characters.");

		//RuleFor(b => b.Authors)
		//	.NotEmpty().WithMessage("At least one {PropertyName} is required");

		//RuleForEach(b => b.Authors)
		//	.SetValidator(new AuthorValidator());

		RuleFor(b => b.Isbn)
			.NotNull();
	}

	private async Task<bool> IsIsbnUniqueAsync(int isbnValue, CancellationToken cancellationToken) 
		=> await _bookRepository.IsUniqueIsbnAsync(isbnValue);
}
