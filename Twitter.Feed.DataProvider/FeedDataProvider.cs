using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Template.Domain.Interfaces;
using Template.Domain.Models;
using Twitter.Feed.Reader.DataProvider.Models;
using Twitter.Feed.Reader.Domain.Models;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Globalization;
using System.Net;

namespace Template.DataProvider
{
    public class FeedDataProvider : IFeedDataProvider
    {
        private readonly ILogger<FeedDataProvider> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TwitterConfig _twitterConfig;
        
        public FeedDataProvider(ILogger<FeedDataProvider> logger,
            IHttpClientFactory httpClientFactory,
            IOptions<TwitterConfig> twitterConfig)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _twitterConfig = twitterConfig.Value ?? throw new ArgumentNullException(nameof(twitterConfig));
        }
        public async Task<List<Feed>> GetFeed(string hashTag)
        {
            _logger.LogInformation($"Retrieveing Feed...");
            List<Feed> feed = null;
            var bearerToken = await GetBearerToken();
            var httpClient = _httpClientFactory.CreateClient("Twitter");
            var query = $"has:hashtags {hashTag}";
            query = WebUtility.UrlEncode(query);
            var searchUrl = $"{_twitterConfig.SearchUrl}?query={query}&start_time={DateTime.Now.Date.AddDays(-1).ToString("o", CultureInfo.GetCultureInfo("en-US"))}&end_time={DateTime.Now.Date.ToString("o", CultureInfo.GetCultureInfo("en-US"))}&max_results=100&tweet.fields=text";
            //searchUrl = WebUtility.UrlEncode(searchUrl);
            //var searchUrl = $"{_twitterConfig.SearchUrl}?query=has:hashtags {hashTag}&max_results=100&tweet.fields=text&start_time={DateTime.Now.AddDays(-1).ToString("s", CultureInfo.GetCultureInfo("en-US"))}&end_time={DateTime.Now.ToString("s", CultureInfo.GetCultureInfo("en-US"))}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, searchUrl);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var httpResponse = await httpClient.SendAsync(requestMessage);

            var content = await httpResponse.Content.ReadAsStringAsync();
            var searchData = JsonConvert.DeserializeObject<SearchData>(content);
            if (searchData.Data != null && searchData.Data.Any())
                feed = (from dataElement in searchData.Data
                        select new Feed { Id = dataElement.Id, Text = dataElement.Text, CreatedAt = dataElement.CreatedAt }).ToList();
            return feed;
        }

        public async Task<string> GetBearerToken()
        {
            var client = _httpClientFactory.CreateClient("Twitter");
            var tokenEndpoint = $"{_twitterConfig.OauthTokenUrl}?grant_type=client_credentials";
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //Post body content
            var authenticationString = $"{_twitterConfig.ApiKey}:{_twitterConfig.ApiKeySecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
          
            //Make the request
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Token>(responseBody);
            return token.AccessToken;
        }
    }
}
