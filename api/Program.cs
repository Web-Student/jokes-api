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

app.Run();
