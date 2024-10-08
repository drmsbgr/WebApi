using WebApiApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/hello", () =>
{
    return "Merhaba Dünya!";
});

app.MapGet("api/person", () =>
{
    // var dict = new Dictionary<string, string>
    // {
    //     { "firstName", "Buğra" },
    //     { "lastName", "DURMUŞ" }
    // };
    // return dict;

    // return new
    // {
    //     FirstName = "Buğra",
    //     LastName = "DURMUŞ",
    //     Age = 19
    // };

    return new Person("Buğra", "Durmuş", 19);
});

app.MapGet("app/date", () =>
{
    var info = new Info(DateTime.Now);

    return info;
});

app.UseHttpsRedirection();
app.Run();
