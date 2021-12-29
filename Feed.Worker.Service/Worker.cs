using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Template.Services.Interfaces;

namespace Twitter.Feed.Reader.Worker.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFeedService _feedService;

        public Worker(ILogger<Worker> logger,
            IFeedService feedService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _feedService = feedService ?? throw new ArgumentNullException(nameof(feedService));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var feed = await _feedService.Get("#cryptocurrencies");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
