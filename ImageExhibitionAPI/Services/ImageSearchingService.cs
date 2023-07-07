using ImageExhibitionAPI.Models;
using Newtonsoft.Json;

namespace ImageExhibitionAPI.Services
{
    public class ImageSearchingService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<dynamic> SearchRequest(string request)
        {
            var searchRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://en.wikipedia.org/w/api.php?action=query&list=search&srsearch={request}&format=json")
            };
            var searchResponse = await _httpClient.SendAsync(searchRequest);
            if (searchResponse.IsSuccessStatusCode)
            {
                var searchResponseContent = await searchResponse.Content.ReadAsStringAsync();
                dynamic returnedContent = JsonConvert.DeserializeObject<SearchResponseModel>(searchResponseContent);
                var response = returnedContent.query.search;
                return response;
            }
            else
            {
                Console.WriteLine("Failed to find matching result");
                return null;
            }
        }
    }
}
