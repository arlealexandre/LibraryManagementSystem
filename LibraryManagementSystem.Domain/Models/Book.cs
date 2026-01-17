public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public ushort PublicationYear { get; set; }
    public string? ISBN { get; set; }
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public int IllustratorId { get; set; }
    public required Illustrator Illustrator { get; set; }

    public bool IsPublicationYearValid()
    {
        return PublicationYear >= 1450 && PublicationYear <= DateTime.Now.Year;
    }

    public bool IsISBNValid()
    {
        if (PublicationYear >= 1970)
        {
            return !string.IsNullOrEmpty(ISBN) && ISBN.Length == 13 && ISBN.All(char.IsDigit);
        }

        // Before 1970, ISBN did not exist, so we can allow the value to be null and return true
        return true;
    }
}