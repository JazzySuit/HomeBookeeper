# Home Library

A household's record of the books it owns, where they are, what state they are in, and the books it wants.

## Language

**Library**:
The single shared collection of one household: its Copies and its Wishlist.
_Avoid_: Collection, catalogue

**Household member**:
A person in the household who uses the Library. All Household members are equal: anyone who can reach the Library can do everything.
_Avoid_: User, account, member

**Title**:
A book as a published work: what it is (title, author, ISBN), independent of any physical item.
_Avoid_: Book, edition, work

**Copy**:
A physical item on a shelf: one instance of a Title that the household owns.
_Avoid_: Book, item, volume

**Room**:
Where a Copy is kept in the home. The only level of location for now.
_Avoid_: Location, shelf, place

**Condition**:
The physical state of a Copy.
_Avoid_: State, quality

**Wishlist**:
The Titles the household wants but has no Copy of.
_Avoid_: Wants, to-buy list

**Genre**:
The kind of content of a Title (e.g. Sci-fi, Science, Baby). A Title has at most one. The Household keeps the list of Genres.
_Avoid_: Type, book type, category

**Genre kind**:
Whether a Genre is Fiction, Non-fiction or Unspecified. A Title's fiction or non-fiction comes from its Genre.
_Avoid_: Fiction flag
