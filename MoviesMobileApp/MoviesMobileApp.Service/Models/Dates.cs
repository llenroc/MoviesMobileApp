using System;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service.Models
{
    public class Dates
    {
        [JsonProperty("maximum")]
        public string Maximum { get; set; }

        [JsonProperty("minimum")]
        public string Minimum { get; set; }
    }
}
