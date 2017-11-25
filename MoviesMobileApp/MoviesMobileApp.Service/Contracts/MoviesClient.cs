using System;
using System.Threading.Tasks;
using MoviesMobileApp.Service.Models;

namespace MoviesMobileApp.Service.Contracts
{
    public sealed class MoviesClient : ServiceClient
    {
        public const string UPCOMING_SEGMENT = "/movie/upcoming";
        public const string RECOMMENDATIONS_SEGMENT = "/movie/{0}/recommendations";
        public const string SEARCH_SEGMENT = "/search/movie";
        public const string DATA_SEGMENT = "?api_key=" + API_KEY + "&page={0}";
        public const string QUERY_DATA_SEGMENT = DATA_SEGMENT+"&query={1}";

        public Task<Response<FeedCollectionData>> GetUpcomingFeedsAsync(int pageNumber) => GetResponseDataAsync<FeedCollectionData>($"{UPCOMING_SEGMENT}{string.Format(DATA_SEGMENT, pageNumber)}");

        public Task<Response<FeedCollectionData>> GetRecommendationsFeedsAsync(int movieId) => GetResponseDataAsync<FeedCollectionData>($"{string.Format(RECOMMENDATIONS_SEGMENT, movieId)}{string.Format(DATA_SEGMENT, 1)}");

        public Task<Response<FeedCollectionData>> GetFeedsByQueryAsync(string query, int pageNumber) => GetResponseDataAsync<FeedCollectionData>($"{SEARCH_SEGMENT}{string.Format(QUERY_DATA_SEGMENT, pageNumber, query)}");
    }
}
