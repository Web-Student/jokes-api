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

app.MapGet("/jokes", async (JokeService service) =>  {
    //Thread.Sleep(5000);
    var response = await service.GetAllJokesAsync();
    return response;
    // return new List<Joke>{ 
    //     new Joke {
    //         Id = 1,
    //         Question = "Have you ever eaten a clock?",
    //         Answer = "It's very time-consuming",
    //         Author = "rachel@mail.com"
    //     }  
    // };
});

app.MapGet("/random", async (JokeService service) =>  {
    //Thread.Sleep(5000);
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

app.Run();
