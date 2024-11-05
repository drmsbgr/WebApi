using ApiApp.Models;
using ApiApp.Models.Errors;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Book Api",
        Version = "v1",
        Description = "book api desc",
        License = new(),
        TermsOfService = new Uri("https://www.youtube.com/@virtual.campus"),
        Contact = new()
        {
            Name = "Buğra Durmuş",
            Email = "drmsbgr@gmail.com",
            Url = new("https://starkinggamestr.blogspot.com")
        },
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature is not null)
        {
            context.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var errorModel = new ErrorModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = contextFeature.Error.Message,
                OccureTime = DateTime.Now
            };

            await context.Response.WriteAsJsonAsync(errorModel);
        }
    });
});

app.UseHttpsRedirection();

app.MapGet("/api/error", () =>
{
    throw new Exception("Bir hata oluştu.");
})
.Produces<ErrorModel>(StatusCodes.Status500InternalServerError)
.ExcludeFromDescription();

app.MapGet("/api/books", () =>
{
    return Book.List.Count == 0
    ? throw new BookNotFoundException()
    : Results.Ok(Book.List);
})
.Produces<List<Book>>(StatusCodes.Status200OK)
.Produces<ErrorModel>(StatusCodes.Status404NotFound)
.WithTags("CRUD", "GETs");

app.MapGet("/api/books/{id:int}", (int id) =>
{
    if (id >= 1 && id <= 1000)
    {
        // Kitap var mı?
        var book = Book
            .List
            .FirstOrDefault(b => b.Id.Equals(id));

        return book is not null
            ? Results.Ok(book)      // 200
            : throw new BookNotFoundException(id);   // 404
    }
    throw new ArgumentOutOfRangeException(nameof(id));
})
.Produces<Book>(StatusCodes.Status200OK)
.Produces<ErrorModel>(StatusCodes.Status404NotFound)
.Produces<ErrorModel>(StatusCodes.Status400BadRequest)
.WithTags("GETs");

app.MapPost("/api/books", (Book newBook) =>
{
    newBook.Id = Book.List.Max(b => b.Id) + 1;    // otomatik
    Book.List.Add(newBook);
    return Results.Created($"/api/books/{newBook.Id}", newBook);
})
.Produces<Book>()
.WithTags("CRUD");

app.MapPut("/api/books/{id:int}", (int id, Book updateBook) =>
{
    if (!(id >= 1 && id <= 1000))
        throw new ArgumentOutOfRangeException(nameof(id));

    var book = Book
                .List
                .FirstOrDefault(b => b.Id.Equals(id)) ?? throw new BookNotFoundException(id);
    book.Title = updateBook.Title;
    book.Price = updateBook.Price;

    return Results.Ok(book);    // 200 
})
.Produces<Book>(StatusCodes.Status200OK)
.Produces<ErrorModel>(StatusCodes.Status404NotFound)
.Produces<ErrorModel>(StatusCodes.Status400BadRequest)
.WithTags("CRUD");

app.MapDelete("/api/books/{id:int}", (int id) =>
{
    if (!(id >= 1 && id <= 1000))
        throw new ArgumentOutOfRangeException(nameof(id));
    var book = Book
        .List
        .FirstOrDefault(b => b.Id.Equals(id)) ?? throw new BookNotFoundException(id);
    Book.List.Remove(book);
    return Results.NoContent();     // 204
})
.Produces(StatusCodes.Status204NoContent)
.Produces<ErrorModel>(StatusCodes.Status404NotFound)
.Produces<ErrorModel>(StatusCodes.Status400BadRequest)
.WithTags("CRUD");

app.MapGet("/api/books/search", (string? title) =>
{
    if (title?.Length < 3)
        throw new ArgumentException("başlık 3 karakterden küçük olamaz");

    var books = string.IsNullOrEmpty(title)
        ? Book.List
        : Book
            .List
            .Where(b => b.Title != null && b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

    return books.Count != 0
        ? Results.Ok(books)     // 200
        : Results.NoContent();  // 204
})
.Produces<List<Book>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status204NoContent)
.Produces<ErrorModel>(StatusCodes.Status400BadRequest)
.WithTags("GETs");

app.Run();