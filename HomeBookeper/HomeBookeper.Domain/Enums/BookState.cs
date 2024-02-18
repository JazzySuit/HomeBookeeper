namespace HomeBookeper.Domain.Enums;

public enum BookState
{
	NotInLibrary = 0,
	OnWishlist,
	InPossession,
	OnLoan,
	NeedsRepair,
	Sold,
	GivenAway,
	Destroyed
}