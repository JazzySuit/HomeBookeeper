using FluentAssertions;
using HomeBookeper.Domain.Entities;
using Xunit;

namespace HomeBookeper.Domain.UnitTests;

public class LibraryTests
{
	// Book state "diagram"
	// ## record cannot be deleted
	// book states: (+wishlist)->(+in possesion)<->(on loan, needs repair)->(sold, given away, destroyed)


	[Fact]
	public void Adding_a_new_book_to_the_library_creates_a_new_book_object()
	{

	}

	[Fact]
	public void Adding_a_new_book_to_the_library_has_a_book_state_as_in_possession()
	{
		
	}

	[Fact]
	public void Wishlisting_a_book_into_the_library_creates_a_new_book_object()
	{

	}

	[Fact]
	public void Wishlisting_a_book_into_the_library_has_a_book_state_as_wishlist()
	{

	}

	[Fact]
	public void Loaning_out_a_book_updates_the_state_of_a_book_to_on_loan_and_who_it_is_loaned_to()
	{
		// is the book lent out & to who?

	}

	[Fact]
	public void Cannot_loan_out_a_book_that_is_already_on_loan()
	{

	}

	[Fact]
	public void Returning_a_book_that_is_on_loan_sets_the_books_state_back_to_in_possession()
	{

	}

	[Fact]
	public void A_book_that_needs_to_be_repaired_has_its_state_set_to_needs_repair()
	{

	}

	[Fact]
	public void When_a_book_is_repaired_the_books_state_is_set_to_in_possession()
	{

	}

	[Fact]
	public void Selling_a_book_sets_a_books_state_to_sold()
	{

	}

	[Fact]
	public void A_book_that_has_been_sold_cannot_be_set_back_to_any_other_book_state()
	{

	}

	[Fact]
	public void Giving_away_a_book_sets_a_books_state_to_given_away()
	{

	}

	[Fact]
	public void A_book_that_has_been_given_away_cannot_be_set_back_to_any_other_book_state()
	{

	}

	[Fact]
	public void Destroying_a_book_sets_a_books_state_to_destroyed()
	{

	}

	[Fact]
	public void A_book_that_has_been_destroyed_cannot_be_set_back_to_any_other_book_state()
	{

	}
}
