namespace HomeBookeper.Domain.Values;

public abstract record SearchableBookProperties { }

public record SearchTitle(string T) : SearchableBookProperties { }

public record SearchAuthor(string First, string Last) : SearchableBookProperties { }


