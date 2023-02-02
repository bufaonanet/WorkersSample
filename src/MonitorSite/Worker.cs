using MonitorSite.Helpers;
using MonitorSite.Models;
using System.IO;
using System.Net;

namespace MonitorSite
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Sites _sites;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _sites = config.GetSection("Sites").Get<Sites>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                HttpStatusCode status = await Requesters.GetStatusFromUrl(_sites.Url);

                string dataString = DateTime.Now.ToString("yyyyMMdd");
                string nameFile = string.Format("logfile_{0}.txt", dataString);
                string path = Path.Combine(@"D:\WorkerServices\", nameFile);
                StreamWriter logFile = new(path, true);

                if (status != HttpStatusCode.OK)
                {                                                     
                    string message = string.Format("O site {0} ficou fora do ar no dia {1}", _sites.Url, dataString);
                    logFile.Write(message);
                    logFile.Close();
                    _logger.LogInformation(message);

                }
                else
                {
                    string message = $"Site está no ar! Verificado há: {DateTimeOffset.Now}";
                    logFile.Write(message);
                    logFile.Close();
                    _logger.LogInformation(message);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}