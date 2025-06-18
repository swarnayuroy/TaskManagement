using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using client.API_Service;
using client.Models;
using client.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace client.DataLayer
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly IService _apiService;
        private HttpResponseMessage _response;
        private Logger<DataAccessLayer> _logger;
        public DataAccessLayer(IService apiService)
        {
            this._apiService = apiService;
            this._logger = new Logger<DataAccessLayer>();
        }

        public async Task<ServiceResponse> CheckCredential(UserCredential credential)
        {
            try
            {
                _response = await _apiService.CheckCredential(credential);
                if (_response.IsSuccessStatusCode)
                {
                    return new ServiceDataResponse<string>
                    {
                        Status = true,
                        StatusCode = _response.StatusCode,
                        Message = $"Token shared to {credential.Email}",
                        Data = JsonConvert.DeserializeObject<string>(_response.Content.ReadAsStringAsync().Result)
                    };
                }
                return new ServiceResponse
                {
                    Status = false,
                    StatusCode = _response.StatusCode,
                    Message = _response.Content != null ? 
                        JObject.Parse(await _response.Content.ReadAsStringAsync())["Message"]?.ToString()
                                : _response.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
                return _response.Content != null ? 
                    new ServiceResponse { Status = false, StatusCode = _response.StatusCode, Message = $"Invalid response." } 
                    : new ServiceResponse { Status = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Failed to process your request" };
            }            
        }

        public async Task<ServiceResponse> RegisterUser(UserRegistration userDetail)
        {
            try
            {
                _response = await _apiService.RegisterUser(userDetail);
                return new ServiceResponse
                {
                    Status = _response.IsSuccessStatusCode ? true : false,
                    StatusCode = _response.StatusCode,
                    Message = _response.Content != null ? _response.StatusCode == System.Net.HttpStatusCode.Created ?
                        JsonConvert.DeserializeObject<string>(_response.Content.ReadAsStringAsync().Result)
                            : JObject.Parse(await _response.Content.ReadAsStringAsync())["Message"]?.ToString()
                                : _response.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
                return _response.Content != null ?
                    new ServiceResponse { Status = false, StatusCode = _response.StatusCode, Message = $"Invalid response." }
                    : new ServiceResponse { Status = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Failed to process your request" };
            }  
        }

        public async Task<ServiceResponse> GetUserDetail(string token, string userId)
        {
            try
            {
                _response = await _apiService.GetUserDetail(token, userId);
                if (_response.IsSuccessStatusCode)
                {
                    return new ServiceDataResponse<UserDetail>
                    {
                        Status = true,
                        StatusCode = _response.StatusCode,
                        Message = $"Detail for {userId} has been found.",
                        Data = JsonConvert.DeserializeObject<UserDetail>(_response.Content.ReadAsStringAsync().Result)
                    };
                }
                return new ServiceResponse
                {
                    Status = false,
                    StatusCode = _response.StatusCode,
                    Message = _response.Content != null ? 
                                JObject.Parse(await _response.Content.ReadAsStringAsync())["Message"]?.ToString()
                                    : _response.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
                return _response.Content != null ?
                    new ServiceResponse { Status = false, StatusCode = _response.StatusCode, Message = $"Invalid response." }
                    : new ServiceResponse { Status = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Failed to process your request" };
            }
        }
    }
}