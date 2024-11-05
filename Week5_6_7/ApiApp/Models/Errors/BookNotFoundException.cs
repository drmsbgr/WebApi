namespace ApiApp.Models.Errors;

public class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(int id) : base($"id:{id} olan bir kitap bulunamadı.")
    {
    }

    public BookNotFoundException() : base("Kitap bulunamadı")
    {
    }
}