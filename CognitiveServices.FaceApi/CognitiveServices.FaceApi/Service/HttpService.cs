using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using CognitiveServices.FaceApi.Extensions;
using CognitiveServices.FaceApi.Models;
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
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", ApplicationJson);
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
            var json = Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, ApplicationJson);
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

        public static async Task ShowError(HttpResponseMessage httpResponseMessage)
        {
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                var json = await new StreamReader(stream).ReadToEndAsync();
                var response = JsonConvert.DeserializeObject<Response>(json);
                await new MessageDialog($"Messagem: {response.Error.Message}", $"Erro: {response.Error.Code}").ShowAsync();
            }
        }
    }
}