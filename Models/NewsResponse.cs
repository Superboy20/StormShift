namespace StormShift.Models
{
    public class NewsResponse
    {
        public List<Article> Articles { get; set; }
    }

    public class Article
    {
        public string Title { get; set; }
        //public string Description { get; set; }
        //public string Url { get; set; }
        //public string UrlToImage { get; set; }
    }
}
