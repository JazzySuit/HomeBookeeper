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
A book as one published edition: what it is (title, authors, ISBN), independent of any physical item. Each format or edition (paperback, hardback, board book) is a separate Title.
_Avoid_: Book, edition, work (a work spanning several editions is not modelled)

**Copy**:
A physical item on a shelf: one instance of a Title that the household owns.
_Avoid_: Book, item, volume

**Room**:
Where a Copy is kept in the home. The only level of location for now.
_Avoid_: Location, shelf, place

**Condition**:
The physical state of a Copy.
_Avoid_: State, quality

**Owned**:
A Title with at least one Copy that is not Gone. A Title whose Copies are all Gone is not Owned.
_Avoid_: In stock, held

**Wishlist**:
The Titles the household wants and does not own. A Title is never both Owned and on the Wishlist; gaining an Owned Copy takes it off.
_Avoid_: Wants, to-buy list

**Genre**:
The kind of content of a Title (e.g. Sci-fi, Science, Baby). A Title has at most one. The Household keeps the list of Genres.
_Avoid_: Type, book type, category

**Genre kind**:
Whether a Genre is Fiction, Non-fiction or Unspecified. A Title's fiction or non-fiction comes from its Genre.
_Avoid_: Fiction flag

**Gone**:
A Copy the household no longer has (given away, lost, worn out). It is kept as a record, not deleted, so the Library remembers it once owned the Title.
_Avoid_: Deleted, archived, removed
