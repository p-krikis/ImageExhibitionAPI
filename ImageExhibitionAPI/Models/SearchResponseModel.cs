namespace ImageExhibitionAPI.Models
{
    public class Query
    {
        public List<ResponseContent> search { get; set; }
    }

    public class SearchResponseModel
    {
        public Query query { get; set; }
    }

    public class ResponseContent
    {
        public string title { get; set; }
        public int pageid { get; set; }
    }
}
