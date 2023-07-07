using ImageExhibitionAPI.Models;
using ImageExhibitionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageExhibitionAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageFetchingService _imageFetchingService;
        private readonly ImageSearchingService _imageSearchingService;
        private readonly ImageSavingService _imageSavingService;

        private static dynamic responseData;

        public ImageController(ImageFetchingService imageFetchingService, ImageSearchingService imageSearchingService, ImageSavingService imageSavingService)
        {
            _imageFetchingService = imageFetchingService;
            _imageSearchingService = imageSearchingService;
            _imageSavingService = imageSavingService;
        }

        [HttpPost("searchQuery")]
        public async Task<IActionResult> SearchQuery([FromBody] UserRequestModel userRequest)
        {
            string userInput = userRequest.Request;
            var matchingQueries = await _imageSearchingService.SearchRequest(userInput);
            responseData = matchingQueries;
            return Ok(matchingQueries);
        }


        [HttpPost("fetchNewImage/{id}")]
        public async Task<IActionResult> FetchData(int id)
        {
            string matchingQuery = responseData[id-1].title;
            //var matchingQuery = await _imageSearchingService.SearchRequest(userInput);
            var imageURL = await _imageFetchingService.FetchImage(matchingQuery);
            var base64 = await _imageFetchingService.ConvertImage(imageURL);
            return Ok(base64);
        }
    }
}
