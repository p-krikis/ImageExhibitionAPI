namespace ImageExhibitionAPI.Models
{
    public class PageResponseModel
    {
        public string title { get; set; }
        public int pageid { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Originalimage originalimage { get; set; }
        public string description { get; set; }
    }

    public class Originalimage
    {
        public string source { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Thumbnail
    {
        public string source { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
