using Data;
using Microsoft.EntityFrameworkCore;


public class JokeDbContext : DbContext {
    public JokeDbContext(DbContextOptions<JokeDbContext> options): base(options)
    {
        
    }
    public DbSet<Joke> Jokes => Set<Joke>();

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.LogTo(Console.WriteLine);  // Log to the console
    // }

}