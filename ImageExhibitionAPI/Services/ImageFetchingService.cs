using ImageExhibitionAPI.Models;
using Newtonsoft.Json;

namespace ImageExhibitionAPI.Services
{
    public class ImageFetchingService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> FetchImage(string request)
        {
            var searchRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://en.wikipedia.org/api/rest_v1/page/summary/{request}")
            };
            var searchResponse = await _httpClient.SendAsync(searchRequest);
            if (searchResponse.IsSuccessStatusCode)
            {
                var searchResponseContent = await searchResponse.Content.ReadAsStringAsync();
                var returnedContent = JsonConvert.DeserializeObject<PageResponseModel>(searchResponseContent);
                string ogImage = returnedContent.thumbnail.source;
                return ogImage;
            }
            else
            {
                Console.WriteLine("Failed to find matching result");
                return null;
            }
        }

        public async Task<string> ConvertImage(string imageURL)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(imageURL))
                {
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await stream.CopyToAsync(ms);
                            byte[] imageBytes = ms.ToArray();
                            string base64string = Convert.ToBase64String(imageBytes);
                            return base64string;
                        }
                    }
                }
            }
        }
        //private async Task<byte[]> GetUrlContentAsync(string url)
        //{
        //    using var httpClient = new HttpClient();
        //    var response = await httpClient.GetAsync(url);
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadAsByteArrayAsync();
        //}
    }
}
