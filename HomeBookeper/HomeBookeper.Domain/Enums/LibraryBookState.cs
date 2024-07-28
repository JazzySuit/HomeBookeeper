namespace HomeBookeper.Domain.Enums;

public enum LibraryBookState
{
	NotInLibrary = 0,
	IsAnAvailableBook,
	RemovedFromLibrary
}

public enum BookStatus
{
	IsAvailable,
	OnLoan,
	NoLongerAvailable
}
