using System.Globalization;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

    public async Task<Joke> AddJokeAsync(Joke newJoke) {
        var context = contextFactory.CreateDbContext();
        var addedJoke = await context.Jokes.AddAsync(newJoke);
        await context.SaveChangesAsync();
        return addedJoke.Entity;
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

    public async Task<bool> DeleteJokeAsync(int id) {
        var context = contextFactory.CreateDbContext();
        var joke = await context.Jokes.Where(j => j.id == id).FirstOrDefaultAsync();
        if (joke is null) {
            return false;
        }
        context.Remove<Joke>(joke);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<AuthorCount>> GetAllAuthorsAsync() {
        var context = contextFactory.CreateDbContext();
        //try {
            var countByAuthors = await context.Jokes.GroupBy(j => j.author)
                .Select(a => new AuthorCount {
                    author = a.Key ?? "", //convert null to empty string to make it easy for react
                    count = a.Count()
                } )
                .OrderByDescending(a => a.count)
                .ToListAsync();
            if (countByAuthors is null) {
                return new List<AuthorCount>();
            }
            // var authorsADifferentWay = await context.Jokes
            //     .Select(j => j.author) // Ensure the property is eagerly loaded
            //     .GroupBy(a => a)
            //     .ToListAsync();
            // foreach (var author in authorsADifferentWay) {
            //     Console.WriteLine("authors a different way" + author);
            // }
            //var authors = await context.Jokes.GroupBy(j => j.author).ToListAsync(); //this is null
            //var countByAuthors = new List<(string, int)>();
            // foreach (var item in authors) {
            //     Console.WriteLine("item is " + item);
            //     //var num = await context.Jokes.Where(j => j.author == item)
            // }
            foreach (var count in countByAuthors) {
                Console.WriteLine("retrieved count by authors as " +  count);
            }
            return countByAuthors;
        //}
        // catch (Exception e) {
        //      Console.WriteLine("caught error " + e.Message.ToString());
        // }
        // var auth = await context.Jokes.ToListAsync();
        // List<string> authorsList = new();
        // foreach (var a in auth) {
        //     Console.WriteLine(a.author);
        //     authorsList.Add(a.author ?? "");
        // }
        // foreach (var a in authorsList) {
        //     Console.WriteLine("list contains " + a);
        // }
        // return new List<(string, int)> ();
    }

    public async Task<IEnumerable<Joke>> GetAllJokesByAuthorAsync(string author)
    {
        var context = contextFactory.CreateDbContext();
        var jokesbyauthor = await context.Jokes.Where(j => j.author == author).ToListAsync();
        return jokesbyauthor;
    }

    internal async Task<Joke> GetJokeByIdAsync(int jokeid)
    {
        var context = contextFactory.CreateDbContext();
        var selectedJoke = await context.Jokes.Where(j => j.id == jokeid).FirstOrDefaultAsync();
        return selectedJoke;
    }
}