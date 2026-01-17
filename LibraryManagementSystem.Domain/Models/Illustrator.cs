public class Illustrator : Person
{
    public ICollection<Book> Books { get; set; } = new List<Book>();
}