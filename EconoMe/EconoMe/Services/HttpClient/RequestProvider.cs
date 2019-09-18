using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Plugin.Connectivity;
using System.Threading;
using System.IO;
using System.Text;
using EconoMe.Exceptions;
using EconoMe.Helpers;

namespace EconoMe.Services.HttpClient
{
    public class RequestProvider : IRequestProvider
    {
        public RequestProvider()
        {
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "", CancellationToken? cancellationToken = null)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    using (var httpClient = CreateHttpClient(token))
                    using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
                    {

                        HttpResponseMessage response;
                        if (cancellationToken.HasValue)
                        {
                            response = await httpClient.GetAsync(uri, cancellationToken.Value);
                        }
                        else
                        {
                            response = await httpClient.GetAsync(uri);
                        }

                        await HandleResponse(response);
                        Stream stream = await response.Content.ReadAsStreamAsync();

                        var result = DeserializeJsonFromsStream<TResult>(stream);

                        return result;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

            throw new NotConnectedException();
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "", CancellationToken? cancellationToken = null)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    using (var httpClient = CreateHttpClient(token))
                    using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
                    {

                        if (!string.IsNullOrEmpty(header))
                        {
                            AddHeaderParameter(httpClient, header);
                        }

                        HttpContent httpContent = null;
                        if (data != null)
                        {
                            var ms = new MemoryStream();
                            SerializeJsonIntoStream(data, ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            httpContent = new StreamContent(ms);
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        }


                        HttpResponseMessage response;
                        if (cancellationToken.HasValue)
                        {
                            response = await httpClient.PostAsync(uri, httpContent, cancellationToken.Value);
                        }
                        else
                        {
                            response = await httpClient.PostAsync(uri, httpContent);
                        }

                        await HandleResponse(response);
                        Stream stream = await response.Content.ReadAsStreamAsync();

                        var result = DeserializeJsonFromsStream<TResult>(stream);

                        return result;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

            throw new NotConnectedException();
        }

        public async Task DeleteAsync(string uri, string token = "")
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    System.Net.Http.HttpClient httpClient = CreateHttpClient(token);
                    await httpClient.DeleteAsync(uri);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

            throw new NotConnectedException();
        }

        private System.Net.Http.HttpClient CreateHttpClient(string token = "", string urlBase = null)
        {
            var httpClient = new System.Net.Http.HttpClient();

            if (!string.IsNullOrEmpty(urlBase))
            {
                httpClient.BaseAddress = new Uri(urlBase);
            }
            else if (!string.IsNullOrEmpty(Settings.UrlBase))
            {
                httpClient.BaseAddress = new Uri(Settings.UrlBase);
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return httpClient;
        }

        private void AddHeaderParameter(System.Net.Http.HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new RequestProviderException(content);
                }

                throw new HttpRequestException(content);
            }
        }

        private static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        private static T DeserializeJsonFromsStream<T>(Stream stream)
        {
            var sr = new StreamReader(stream);
            var jtr = new JsonTextReader(sr);
            var js = new JsonSerializer()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            js.Converters.Add(new StringEnumConverter());
            var result = js.Deserialize<T>(jtr);
            return result;
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }
    }
}
