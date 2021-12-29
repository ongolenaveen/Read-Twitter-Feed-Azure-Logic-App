using System.Threading.Tasks;
using Template.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Template.Domain.Models;
using Template.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Template.Services
{
    public class FeedService : IFeedService
    {
        private readonly ILogger<FeedService> _logger;
        private readonly IFeedDataProvider _hostDataProvider;
        public FeedService(ILogger<FeedService> logger, IFeedDataProvider feedDataProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hostDataProvider = feedDataProvider ?? throw new ArgumentNullException(nameof(feedDataProvider));
        }
        public async Task<List<Feed>> Get(string hashTag)
        {
            _logger.LogInformation($"Executing Feed Service");
            var response = await _hostDataProvider.GetFeed(hashTag);
            _logger.LogInformation($"Finished Executing Feed Service. Sending {response}");
            return response;
        }
    }
}
