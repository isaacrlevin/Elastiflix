namespace Web
{   public class SearchResult
    {
        public Meta meta { get; set; }
        public Result[] results { get; set; }
    }

    public class Meta
    {
        public object[] alerts { get; set; }
        public object[] warnings { get; set; }
        public int precision { get; set; }
        public Page page { get; set; }
        public Engine engine { get; set; }
        public string request_id { get; set; }
    }

    public class Page
    {
        public int current { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public int size { get; set; }
    }

    public class Engine
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Result
    {
        public Original_Language original_language { get; set; }
        public Imdb_Id imdb_id { get; set; }
        public Video video { get; set; }
        public Title title { get; set; }
        public Backdrop_Path backdrop_path { get; set; }
        public Cast cast { get; set; }
        public Revenue revenue { get; set; }
        public User_Score user_score { get; set; }
        public Genres genres { get; set; }
        public Popularity popularity { get; set; }
        public Production_Countries production_countries { get; set; }
        public _Meta _meta { get; set; }
        public Id id { get; set; }
        public Vote_Count vote_count { get; set; }
        public Budget budget { get; set; }
        public Overview overview { get; set; }
        public Runtime runtime { get; set; }
        public Poster_Path poster_path { get; set; }
        public Spoken_Languages spoken_languages { get; set; }
        public Production_Companies production_companies { get; set; }
        public Release_Date release_date { get; set; }
        public Belongs_To_Collection belongs_to_collection { get; set; }
        public Tagline tagline { get; set; }
        public Adult adult { get; set; }
        public Homepage homepage { get; set; }
        public Status status { get; set; }
        public Rating rating { get; set; }
    }

    public class Original_Language
    {
        public string raw { get; set; }
    }

    public class Imdb_Id
    {
        public string raw { get; set; }
    }

    public class Video
    {
        public string raw { get; set; }
    }

    public class Title
    {
        public string raw { get; set; }
    }

    public class Backdrop_Path
    {
        public string raw { get; set; }
    }

    public class Cast
    {
        public string[] raw { get; set; }
    }

    public class Revenue
    {
        public float raw { get; set; }
    }

    public class User_Score
    {
        public float raw { get; set; }
    }

    public class Genres
    {
        public string[] raw { get; set; }
    }

    public class Popularity
    {
        public float raw { get; set; }
    }

    public class Production_Countries
    {
        public string[] raw { get; set; }
    }

    public class _Meta
    {
        public string id { get; set; }
        public string engine { get; set; }
        public object score { get; set; }
    }

    public class Id
    {
        public string raw { get; set; }
    }

    public class Vote_Count
    {
        public float raw { get; set; }
    }

    public class Budget
    {
        public float raw { get; set; }
    }

    public class Overview
    {
        public string raw { get; set; }
    }

    public class Runtime
    {
        public float? raw { get; set; }
    }

    public class Poster_Path
    {
        public string raw { get; set; }
    }

    public class Spoken_Languages
    {
        public string[] raw { get; set; }
    }

    public class Production_Companies
    {
        public string[] raw { get; set; }
    }

    public class Release_Date
    {
        public DateTime raw { get; set; }
    }

    public class Belongs_To_Collection
    {
        public string raw { get; set; }
    }

    public class Tagline
    {
        public string raw { get; set; }
    }

    public class Adult
    {
        public string raw { get; set; }
    }

    public class Homepage
    {
        public string raw { get; set; }
    }

    public class Status
    {
        public string raw { get; set; }
    }

    public class Rating
    {
        public string raw { get; set; }
    }

}
