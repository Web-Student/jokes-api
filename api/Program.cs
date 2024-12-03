using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddDbContextFactory<JokeDbContext>(opt => opt.UseNpgsql(builder.Configuration["database"]));

builder.Services.AddScoped<JokeService>();
var app = builder.Build();
app.UseCors(config => 
    config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
);

app.MapGet("/", () => "Hello World!");

app.MapGet("/authors", async (JokeService service) =>  {
    var response = await service.GetAllAuthorsAsync();
    return response;
});

app.MapGet("/jokes", async (JokeService service) =>  {
    //Thread.Sleep(5000);
    var response = await service.GetAllJokesAsync();
    return response;
});

app.MapGet("/jokes/{jokeid:int}", async (int jokeid, JokeService service) =>  {
    var response = await service.GetJokeByIdAsync(jokeid);
    return response;
});


app.MapGet("/jokesbyauthor/{author}", async (string author, JokeService service) => {
    var response = await service.GetAllJokesByAuthorAsync(author);
});

app.MapGet("/random", async (JokeService service) =>  {
    var response = await service.GetRandomJokeAsync();
    if (response.id < 0) {
        throw new Exception("failed to fetch random joke");
    }
    return response;
});

app.MapPost("/add", async (Joke newJoke, JokeService service) => {
    var joke = await service.AddJokeAsync(newJoke);
    return joke.id;
});

app.MapDelete("/delete/{id:int}", async (int id, JokeService service) => {
    Console.WriteLine($"DELETE endpoint hit with id = {id}");
    var successful = await service.DeleteJokeAsync(id);
    return successful;
});

app.Run();
