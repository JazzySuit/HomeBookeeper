using FluentValidation;
using FluentValidation.Results;

namespace HomeBookeper.Domain.Entities
{
	public abstract class Book
	{
		private readonly string _title;
		private readonly Author _author;

		private readonly BookValidator _validator;

		public Book(string title, Author author)
		{
			_title = title;
			_author = author;

			_validator = new BookValidator();
		}

		public string Title => _title;

		// TODO: make as a collection
		public Author Author => _author;

		// TODO: have metadata on a book series
		//public Series Series

		// TODO: make ISBN number a value type
		public int ISBN { get; set; }

		public ValidationResult Validate() => _validator.Validate(this);

		internal class BookValidator : AbstractValidator<Book>
		{
			public BookValidator()
			{
				RuleFor(book => book.Title).NotEmpty();
				RuleFor(book => book.Author).NotNull();
				RuleFor(book => book.Author).SetValidator(new AuthorValidator());
			}
		}


	}
}