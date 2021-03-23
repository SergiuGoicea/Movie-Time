using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieTime.Data.Model
{
    public class MovieList
    {

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public IList<Movie> Results { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }

}
