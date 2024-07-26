namespace HomeBookeper.Domain.Values;

public abstract record SearchableBookProperties { }

public record Title(string T) : SearchableBookProperties { }

public record AuthorName(string First, string Last) : SearchableBookProperties { }


