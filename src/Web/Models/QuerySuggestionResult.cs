namespace Web
{
    public class QuerySuggestionResult
    {
        public Results results { get; set; }
        public Meta meta { get; set; }
    }

    public class Results
    {
        public Document[] documents { get; set; }
    }

    public class Document
    {
        public string suggestion { get; set; }
    }
}
