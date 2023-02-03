using Microsoft.EntityFrameworkCore;
using SeedDbWorker.Models;

namespace SeedDbWorker.Services;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
}
