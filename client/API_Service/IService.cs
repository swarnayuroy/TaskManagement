using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using client.Models;

namespace client.API_Service
{
    public interface IService
    {
        #region POST
        Task<HttpResponseMessage> CheckCredential(UserCredential credential);
        Task<HttpResponseMessage> RegisterUser(UserRegistration detail);
        #endregion

        #region GET
        Task<HttpResponseMessage> GetUserDetail(string token, string userId);
        #endregion
    }
}
