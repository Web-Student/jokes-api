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
}