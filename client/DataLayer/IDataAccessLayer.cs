using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.Models;

namespace client.DataLayer
{
    public interface IDataAccessLayer
    {
        Task<ServiceResponse> CheckCredential(UserCredential credential);
        Task<ServiceResponse> RegisterUser(UserRegistration detail);
        Task<ServiceResponse> GetUserDetail(string token, string userId);
    }
}
