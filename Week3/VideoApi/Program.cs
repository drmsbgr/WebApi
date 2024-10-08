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

app.MapGet("api/videos", () => Video.Videos)
.WithDisplayName("GetVideo")
.WithOpenApi();

app.MapPost("api/videos", (Video item) => Video.Videos.Add(item));
app.MapPut("api/videos", (int id, Video video) =>
{
    var item = Video.Videos.First(x => x.Id == id);
    if (item is not null)
    {
        item.Title = video.Title;
        item.Duration = video.Duration;
        return "Video Düzenlendi.";
    }
    else
        return "Video Bulunamadı!";
});
app.MapDelete("api/videos", (int id) => Video.Videos.RemoveById(id));

app.Run();
