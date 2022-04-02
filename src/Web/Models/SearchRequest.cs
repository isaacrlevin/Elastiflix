using System.Text;

namespace Web
{
    public class SearchRequest
    {
        public string query { get; set; }
        public List<Sort> sort { get; set; }

        public Page page { get; set; }

        public List<Filter> filters { get; set; }

        public static string BuildSearchRequest(SearchRequest request)
        {
            StringBuilder stringRequest = new StringBuilder();

            if ((request.query == null))
            {
                throw new Exception();
            }

            stringRequest.Append("{\"query\": \"" + request.query + "\",");

            if (request.sort != null && request.sort.Count > 0)
            {
                stringRequest.Append("\"sort\": [");
                foreach (var sortItem in request.sort)
                {
                    string f = "{ \"" + sortItem.field + "\": \"" + sortItem.direction + "\" }";
                    if (sortItem != request.sort.Last())
                    {
                        f = f + ",";
                    }
                    else
                    {
                        stringRequest.Append(f);
                    }
                }
                stringRequest.Append("],");
            }

            if (request.filters != null && request.filters.Count > 0)
            {
                stringRequest.Append("\"filters\": {");
                stringRequest.Append("\"all\": [{");
                foreach (var filterItem in request.filters)
                {
                    stringRequest.Append("\"all\": [");
                    foreach (var subItem in filterItem.values)
                    {
                        if (filterItem.field == "user_score")
                        {
                            string from = subItem.Split(" ").FirstOrDefault();
                            string to = subItem.Split(" ").LastOrDefault();

                            string score = "{ \"user_score\":  {\"to\":" + to + " ,\"from\":" + from + "}}";
                            stringRequest.Append(score);
                        }
                        else if (filterItem.field == "runtime")
                        {
                            if (filterItem.values.FirstOrDefault() == "Less than an hour")
                            {
                                stringRequest.Append("{ \"runtime\":  {\"to\": 60 ,\"from\": 0}}");
                            }
                            else
                            {
                                stringRequest.Append("{ \"runtime\":  {\"from\": 60}}");
                            }
                        }
                        else
                        {
                            stringRequest.Append("{ \"" + filterItem.field + "\": \"" + subItem + "\" }");
                        }
                        if (subItem != filterItem.values.Last())
                        {
                            stringRequest.Append(",");
                        }
                    }
                    if (filterItem != request.filters.Last())
                    {
                        stringRequest.Append(",");
                    }
                    stringRequest.Append("]");
                }
                stringRequest.Append("}]},");



                /*
                 *"filters": {
	"all": [{
		"all": [
                {			"cast": "Anthony Daniels"		}, 
                {			"cast": "Alec Guinness"		}
                ]
	}, {
		"all": [{
			"genres": "Action"
		}, {
			"genres": "Adventure"
		}]
	}]
}
                */

            }

            if (request.page != null)
            {
                stringRequest.Append("\"page\": { \"current\": " + request.page.current + ", \"size\": " + request.page.size + "}");

            }

            stringRequest.Append("}");
            return stringRequest.ToString();

        }
    }

    public class Filter
    {
        public string field { get; set; }
        public List<string> values { get; set; }
    }

    public class Sort
    {
        public string field { get; set; }
        public string direction { get; set; }
    }
}
