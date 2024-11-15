using Data;
using Microsoft.EntityFrameworkCore;

public class JokeService{
    private IDbContextFactory<JokeDbContext> contextFactory;
    public JokeService(IDbContextFactory<JokeDbContext> factory) {
        this.contextFactory = factory;
    }

    public async Task<IEnumerable<Joke>> GetAllJokesAsync() {
        var context = contextFactory.CreateDbContext();
        var jokeList = await context.Jokes.ToListAsync();
        return jokeList;
    }

    public async Task<Joke> GetRandomJokeAsync() {
        var context = contextFactory.CreateDbContext();
        var jokeList = await context.Jokes.ToListAsync();
        int rand = Random.Shared.Next(0, jokeList.Count);
        return jokeList[rand];
    }
    public async Task<Joke> EditJokeAsync(int id, Joke updatedJoke) {
        var context = contextFactory.CreateDbContext();
        var dbjoke = await context.Jokes.Where(j => j.id == id).FirstOrDefaultAsync();
        if (dbjoke is not null) {
            dbjoke.answer = updatedJoke.answer;
            dbjoke.author = updatedJoke.author;
            dbjoke.question = updatedJoke.question;
            await context.SaveChangesAsync();
            return dbjoke;
        }
        else {
            //negative 1 status to indicate error. Or, should I throw an exception?
            return new Joke {
                id = -1
            };
        }
    }
}