using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CognitiveServices.FaceApi.Extensions;
using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Service
{
    public static class HttpService
    {
        private const string BaseUrl = "";
        private const string SubscriptioKey = "";
        private const string ApplicationJson = "application/json";

        private static HttpClient Headers(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptioKey);
            return httpClient;
        }

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = new HttpClient();
            httpClient = Headers(httpClient);
            return await httpClient.GetAsync($"{BaseUrl}/{url}");
        }

        public static async Task<HttpResponseMessage> PostAsync(string url, object obj)
        {
            var content = new StringContent(Serialize(obj), Encoding.UTF8, ApplicationJson);
            var httpClient = new HttpClient();
            httpClient = Headers(httpClient);
            return await httpClient.PostAsync($"{BaseUrl}/{url}", content);
        }

        public static async Task<HttpResponseMessage> PatchAsync(string url, object obj)
        {
            var content = new StringContent(Serialize(obj), Encoding.UTF8, ApplicationJson);
            var httpClient = new HttpClient();
            httpClient = Headers(httpClient);
            return await httpClient.PatchAsync($"{BaseUrl}/{url}", content);
        }

        public static async Task<HttpResponseMessage> PutAsync(string url, object obj)
        {
            var content = new StringContent(Serialize(obj), Encoding.UTF8, ApplicationJson);
            var httpClient = new HttpClient();
            httpClient = Headers(httpClient);
            return await httpClient.PutAsync($"{BaseUrl}/{url}", content);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var httpClient = new HttpClient();
            httpClient = Headers(httpClient);
            return await httpClient.DeleteAsync($"{BaseUrl}/{url}");
        }
    }
}