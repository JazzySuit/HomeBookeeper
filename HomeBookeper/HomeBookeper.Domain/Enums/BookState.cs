namespace HomeBookeper.Domain.Enums;

public enum BookState
{
	NotInLibrary = 0,
	OnWishlist,
	InPossession,
	Removed
}

public enum LibraryBookState
{
	InLibrary,
	OnLoan,
	NeedsRepair,
	Sold,
	GivenAway,
	Destroyed
}
