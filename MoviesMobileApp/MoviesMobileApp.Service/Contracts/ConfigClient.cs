using System;
using System.Threading.Tasks;
using MoviesMobileApp.Service.Models;

namespace MoviesMobileApp.Service.Contracts
{
    public class ConfigClient : ServiceClient
    {
        public const string UPCOMING_SEGMENT = "/configuration";
        public const string DATA_SEGMENT = "?api_key=" + API_KEY;

        public Task<Reponse<ServiceConfiguration>> GetServiceConfigAsync() => GetResponseDataAsync<ServiceConfiguration>($"{UPCOMING_SEGMENT}{DATA_SEGMENT}");
    }
}
