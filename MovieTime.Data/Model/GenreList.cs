using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieTime.Data.Model
{
    public class GenreList
    {

        [JsonProperty("genres")]
        public IList<Genres> genres { get; set; }
    }

}
