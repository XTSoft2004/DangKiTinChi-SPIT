using HelperHttpClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HelperHttpClient
{
    public partial class RequestHttpClient
    {
        // HTTP Methods
        public static async Task<string> GetTextContent(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage == null)
                return string.Empty;

            byte[] buffer = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            string content = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            return content;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            try
            {
                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.GetAsync(url, _cancellationToken) : await _client.GetAsync(url);
                Response = response;
                Content = await GetTextContent(response);
                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, string json)
        {
            try
            {
                json = json.Replace("\r", "").Replace("\n", "").Trim();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.PostAsync(url, content, _cancellationToken) : await _client.PostAsync(url, content);
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> dataPost)
        {
            try
            {
                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.PostAsync(url, new FormUrlEncodedContent(dataPost), _cancellationToken) : 
                    await _client.PostAsync(url, new FormUrlEncodedContent(dataPost));
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, MultipartFormData multipartForm)
        {
            try
            {
                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.PostAsync(url, multipartForm.content, _cancellationToken) : 
                    await _client.PostAsync(url, multipartForm.content);
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, MultipartFormDataContent multipartFormData)
        {
            try
            {
                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.PostAsync(url, multipartFormData, _cancellationToken) : 
                    await _client.PostAsync(url, multipartFormData);
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url)
        {
            try
            {
                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.PostAsync(url, null, _cancellationToken) : await _client.PostAsync(url, null);
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, dynamic? DataPost)
        {
            try
            {
                string json = JsonConvert.SerializeObject(DataPost);
                json = json.Replace("\r", "").Replace("\n", "").Trim();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.PostAsync(url, content, _cancellationToken) : await _client.PostAsync(url, content);
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            try
            {
                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.DeleteAsync(url, _cancellationToken) : await _client.DeleteAsync(url);
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, dynamic? DataPost)
        {
            try
            {
                string json = JsonConvert.SerializeObject(DataPost);
                json = json.Replace("\r", "").Replace("\n", "").Trim();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _cancellationToken != default(CancellationToken) ?
                    await _client.PatchAsync(url, content, _cancellationToken) : await _client.PatchAsync(url, content);
                Response = response;
                Content = await GetTextContent(response);

                return response;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Console.WriteLine("Yêu cầu buộc huỷ do timeout.");
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra:{ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}