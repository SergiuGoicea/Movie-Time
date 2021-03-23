using System;
using Newtonsoft.Json;

namespace MovieTime.Data.Model
{

    public class Genres
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }


}
