using System;
using System.Threading.Tasks;
using MoviesMobileApp.Service.Models;

namespace MoviesMobileApp.Service.Contracts
{
    public sealed class MoviesClient : ServiceClient
    {
        public const string UPCOMING_SEGMENT = "/movie/upcoming";
        public const string DATA_SEGMENT = "?api_key=" + API_KEY + "&page={0}";

        public Task<Reponse<UpcomingFeeds>> GetUpcomingFeedsAsync(int pageNumber) => GetResponseDataAsync<UpcomingFeeds>($"{UPCOMING_SEGMENT}{string.Format(DATA_SEGMENT, pageNumber)}");
    }
}
