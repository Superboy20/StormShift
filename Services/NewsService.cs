namespace StormShift.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using StormShift.Models;

    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NewsService> _logger;

        public NewsService(HttpClient httpClient, IConfiguration configuration, ILogger<NewsService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://newsapi.org/v2");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Article>> GetTopHeadlines(string country = "us")
        {
            string apiKey = _configuration["ApiKeys:NewsApi"];
            string endpoint = $"everything?country={country}&apiKey={apiKey}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NewsResponse>(content);

                    return result.Articles;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching news data.");
            }

            // Return an empty list if there's an error or the response is not successful.
            return new List<Article>();
        }
    }
}
