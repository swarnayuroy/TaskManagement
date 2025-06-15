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
using client.Utils;
using Newtonsoft.Json;
using System.Text;

namespace client.API_Service.DomainService
{
    public class Service : IService
    {
        private readonly HttpClient _client;
        private Logger<Service> _logger;
        public Service()
        {
            this._logger = new Logger<Service>();
            this._client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            this._client.BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceUri"]);
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region POST Service
        public async Task<HttpResponseMessage> CheckCredential(UserCredential credential)
        {
            try
            {
                StringContent userCredential = new StringContent(JsonConvert.SerializeObject(credential), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/account/check", userCredential);
                _logger.LogDetails(LogType.INFO, response.StatusCode.ToString());

                return response;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
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
                _logger.LogDetails(LogType.INFO, response.StatusCode.ToString());

                return response;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
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
                _logger.LogDetails(LogType.INFO, response.StatusCode.ToString());
                
                return response;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
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