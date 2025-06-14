using microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using microservice.Utils;
using microservice.DTO;

namespace microservice.Service
{
    public interface IAccountService : IService<User>
    {
        Task<ResponseDetail> Check(string email, string password);
        Task<ResponseDetail> Register(UserDTO userDetails);
    }
}
