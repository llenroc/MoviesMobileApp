using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service.Models
{
    public class ServiceConfiguration
    {
        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("change_keys")]
        public List<string> ChangeKeys { get; set; }
    }
}
