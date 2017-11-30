using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service.Models
{
    public class ServiceConfiguration
    {
        public Images Images { get; set; }

        public List<string> ChangeKeys { get; set; }
    }
}
