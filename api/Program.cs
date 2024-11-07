using Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/jokes", () => new Joke{
    Id = 1,
    Question = "Have you ever eaten a clock?",
    Answer = "It's very time-consuming",
    Author = "rachel@mail.com"
});

app.Run();
