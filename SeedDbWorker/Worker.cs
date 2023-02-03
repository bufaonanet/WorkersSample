using SeedDbWorker.Models;
using SeedDbWorker.Services;

namespace SeedDbWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DbHelper dbHelper;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            dbHelper = new DbHelper();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                List<User> users = dbHelper.GetAllUsers();
                if (users.Any())
                {
                    PrintUserInfo(users);
                }
                else
                {
                    dbHelper.SeedData();
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void PrintUserInfo(List<User> users)
        {
            users.ForEach(user =>
            {
                _logger.LogInformation($"User info: Name: {user.Name}  and addres: {user.Address}");
            });
        }
    }
}