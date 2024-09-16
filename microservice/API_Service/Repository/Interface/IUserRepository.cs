using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Service.Repository.Interface
{
    public interface IUserRepository
    {
        Task<bool> AddUser(Models.User user);
        Task<Models.User> GetUser(Guid id);
        Task<bool> ChangeCredential(Guid userId, string password);
        Task<bool> CheckValidUser(string email, string password);
    }
}
