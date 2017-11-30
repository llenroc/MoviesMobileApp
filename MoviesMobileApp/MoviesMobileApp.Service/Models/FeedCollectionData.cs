using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service.Models
{
    public class FeedCollectionData
    {
        public List<Feed> Results { get; set; }

        public int Page { get; set; }

        public int TotalResults { get; set; }

        public Dates Dates { get; set; }

        public int TotalPages { get; set; }
    }
}
