namespace Web
{
   public class SearchBarResultView
    {
        public string Text { get; set; }
        public ResultType ResultType { get; set; }

    }

    public enum ResultType
    {
        Search,
        Suggestion
    }

}
