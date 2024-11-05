using MovieApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/movies", () => Results.Ok(Movie.Movies));

app.MapGet("api/movies/{id:int}", (int id) =>
{
    var movie = Movie.Movies.Find(x => x.Id.Equals(id));
    return movie is not null ? Results.Ok(movie) : Results.NotFound();
});

app.MapPost("api/movies", (string title, decimal duration) =>
{
    var movie = new Movie(title, duration);
    Movie.AddMovie(movie);
    return Results.Created($"/api/movies/{movie.Id}", movie);
});

app.MapGet("api/movies/search", (string? title) =>
{
    var movies = string.IsNullOrEmpty(title) ? Movie.Movies : Movie.Movies.Where(m => m.Title != null && m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
    return movies.Count != 0 ? Results.Ok(movies) : Results.NoContent();
});

app.MapPut("api/movies", (Movie edited) => Movie.EditMovie(edited));

app.MapDelete("api/movies", (int id) => Movie.RemoveMovieById(id));

app.Run();
