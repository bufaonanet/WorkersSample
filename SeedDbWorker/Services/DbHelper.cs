using Microsoft.EntityFrameworkCore;
using SeedDbWorker.Models;

namespace SeedDbWorker.Services;

public class DbHelper
{
    private AppDbContext _dbContext;

    private DbContextOptions<AppDbContext> GetAllOptions()
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionBuilder.UseSqlServer(AppSettings.ConnectionString);
        return optionBuilder.Options;
    }

    public List<User> GetAllUsers()
    {
        using (_dbContext = new AppDbContext(GetAllOptions()))
        {
            try
            {
                var users = _dbContext.Users.ToList();
                if (users != null && users.Any())
                {
                    return users;
                }
                else
                {
                    return new List<User>();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public void SeedData()
    {
        using (_dbContext = new AppDbContext(GetAllOptions()))
        {
            _dbContext.Users.Add(new User
            {
                Name = "Douglas",
                Address = "Rua qualquer por ai"
            });
            _dbContext.SaveChanges();
        }
    }
}