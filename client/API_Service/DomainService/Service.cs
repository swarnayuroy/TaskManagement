using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Threading.Tasks;
using client.Models;
using Newtonsoft.Json;
using System.Text;

namespace client.API_Service.DomainService
{
    public class Service : IService
    {
        private readonly HttpClient _client;
        public Service()
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            _client.BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceUri"]);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region POST Service
        public async Task<HttpResponseMessage> CheckCredential(UserCredential credential)
        {
            try
            {
                StringContent userCredential = new StringContent(JsonConvert.SerializeObject(credential), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/account/check", userCredential);

                return response;
            }
            catch (TaskCanceledException ex)
            {
                if (ex.InnerException is TimeoutException)
                {
                    return new HttpResponseMessage(HttpStatusCode.RequestTimeout) { ReasonPhrase = "Request timeout" };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Request cancelled" };
                }
            }
        }
        public async Task<HttpResponseMessage> RegisterUser(UserRegistration detail)
        {
            try
            {
                StringContent userDetail = new StringContent(JsonConvert.SerializeObject(detail), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/account/register", userDetail);

                return response;
            }
            catch (TaskCanceledException ex)
            {
                if (ex.InnerException is TimeoutException)
                {
                    return new HttpResponseMessage(HttpStatusCode.RequestTimeout) { ReasonPhrase = "Request timeout" };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Request cancelled" };
                }
            }
        }
        #endregion

        #region GET Service
        public async Task<HttpResponseMessage> GetUserDetail(string token, string userId)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}/user/get/{userId}");

                return response;
            }
            catch (TaskCanceledException ex)
            {
                if (ex.InnerException is TimeoutException)
                {
                    return new HttpResponseMessage(HttpStatusCode.RequestTimeout) { ReasonPhrase = "Request timeout" };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Request cancelled" };
                }
            }
        }
        #endregion
    }
}