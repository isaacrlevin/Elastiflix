namespace Web
{
    public class QuerySuggestionRequest
    {
        public string query { get; set; }
        public Types types { get; set; }
        public int size { get; set; }

        public class Types
        {
            public Documents documents { get; set; }
        }

        public class Documents
        {
            public string[] fields { get; set; }
        }

    }
}
