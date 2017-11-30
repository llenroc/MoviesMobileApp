using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service.Models
{
    public class Images
    {
        public string BaseUrl { get; set; }

        public string SecureBaseUrl { get; set; }

        public List<string> BackdropSizes { get; set; }

        public List<string> LogoSizes { get; set; }

        public List<string> PosterSizes { get; set; }

        public List<string> ProfileSizes { get; set; }

        public List<string> StillSizes { get; set; }
    }
}
