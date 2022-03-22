using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Data;

public class ShortenerContext : DbContext
{
    public DbSet<Link> Links { get; set; }
    
    public ShortenerContext(DbContextOptions<ShortenerContext> options): base(options)
    {
        Database.EnsureCreated();
    }
}