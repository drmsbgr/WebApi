using VideoLib.Models;

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

app.MapGet("/video", () =>
{
    var videos = new PlayList
    {
        new(1, "Heavy Blanket", 3.52),
        new(2, "The Summoning", 4.20),
        new(3, "Lovebite", 3.24),
        new(3, "Like A Villain", 3.54),
        new(3, "Bring Me To Life", 3.49)
    };

    return videos;
}).WithDisplayName("GetVideo")
.WithOpenApi();

app.Run();
