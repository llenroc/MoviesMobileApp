using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service.Models
{
    public class UpcomingFeeds
    {
        [JsonProperty("results")]
        public List<Feed> Results { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("dates")]
        public Dates Dates { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}
