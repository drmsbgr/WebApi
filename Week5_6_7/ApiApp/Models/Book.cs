namespace ApiApp.Models;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public decimal Price { get; set; }

    // Seed data
    private static readonly List<Book> _bookList =
    [
        new Book { Id = 1, Title = "İnce Memed", Price = 20.00M },
        new Book { Id = 2, Title = "Kuyucaklı Yusuf", Price = 15.50M },
        new Book { Id = 3, Title = "Çalıkuşu", Price = 18.75M }
    ];

    public static List<Book> List => _bookList;
}