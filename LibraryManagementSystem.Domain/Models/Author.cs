public class Author : Person
{
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}