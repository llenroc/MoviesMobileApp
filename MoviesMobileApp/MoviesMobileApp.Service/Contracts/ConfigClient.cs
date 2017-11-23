using System;
using System.Threading.Tasks;
using MoviesMobileApp.Service.Models;

namespace MoviesMobileApp.Service.Contracts
{
    public class ConfigClient : ServiceClient
    {
        public const string CONFIG_SEGMENT = "/configuration";
        public const string GENRES_SEGMENT = "/genre/movie/list";
        public const string DATA_SEGMENT = "?api_key=" + API_KEY;

        public Task<Reponse<ServiceConfiguration>> GetServiceConfigAsync() => GetResponseDataAsync<ServiceConfiguration>($"{CONFIG_SEGMENT}{DATA_SEGMENT}");

        public Task<Reponse<GenresData>> GetGenresAsync() => GetResponseDataAsync<GenresData>($"{GENRES_SEGMENT}{DATA_SEGMENT}");
    }
}
