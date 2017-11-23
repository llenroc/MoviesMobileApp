using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service.Models
{
    public class GenresData
    {
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }
    }
}
