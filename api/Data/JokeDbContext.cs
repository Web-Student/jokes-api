using Data;
using Microsoft.EntityFrameworkCore;


class JokeDbContext(DbContextOptions<JokeDbContext> options) : DbContext(options) {
    public DbSet<Joke> Jokes => Set<Joke>();

}