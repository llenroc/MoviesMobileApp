using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoviesMobileApp.Service
{
    public abstract class ServiceClient
    {
        public const string BASE_URL = "https://api.themoviedb.org";
        public const string API_VER = "/3";
        internal const string API_KEY = "1f54bd990f1cdfb230adb312546d765d";
        
        static int _timeoutMilliseconds = 10000;
        static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BASE_URL),
            Timeout = TimeSpan.FromMilliseconds(_timeoutMilliseconds)
        };

        internal ServiceClient()
        {
        }

        internal Task<HttpResponseMessage> GetDataAsync(string endpoint) => _httpClient.GetAsync(endpoint);

        internal string SerializeObject(object obj) => JsonConvert.SerializeObject(obj);

        internal T DeSerializeObject<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        internal async Task<Reponse<T>> GetResponseDataAsync<T>(string segment)
        {
            var result = await GetDataAsync(API_VER + segment);
            var responseMessage = await result.Content.ReadAsStringAsync();
            T resultItem = default(T);
            string errorMessage = null;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                resultItem = DeSerializeObject<T>(responseMessage);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                errorMessage = "Unauthorized";
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                errorMessage = "NotFound";
            }
            return new Reponse<T>(resultItem, result.StatusCode, errorMessage);
        }
    }
}
