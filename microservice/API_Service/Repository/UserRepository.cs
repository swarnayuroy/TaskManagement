using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using API_Service.Models;
using API_Service.Data;
using API_Service.Repository.Interface;

namespace API_Service.Repository
{
    public class UserRepository : IUserRepository
    {
        private static IList<User> _users;
        private static IList<UserLog> _userLogs;
        public UserRepository()
        {
            _users = SampleData.users;
            _userLogs = SampleData.userLogs;
        }
        public async Task<User> GetUser(Guid id)
        {
            User user;
            try
            {
                user = await System.Threading.Tasks.Task.Run(() =>
                {
                    return _users.Where(usr => usr.Id == id).SingleOrDefault<User>();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }
        public async Task<bool> AddUser(User user)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    user.Id = Guid.NewGuid();
                    _users.Add(user);
                    _userLogs.Add(new UserLog()
                    {
                        UserId = user.Id,
                        RegisteredAt = System.DateTime.Now
                    });
                    return true;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveUsersAsync(_users);
                    await SampleData.SaveUserLogsAsync(_userLogs);
                }
            }
            return response;
        }
        public async Task<bool> ChangeCredential(Guid userId, string password)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    var user = _users.Where(usr => usr.Id == userId).SingleOrDefault<User>();
                    if (user != null)
                    {
                        _users[_users.IndexOf(user)].Password = password;
                    }
                    return true;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveUsersAsync(_users);
                }
            }
            return response;
        }
        public async Task<bool> CheckValidUser(string email, string password)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    var user = _users.Where(usr => usr.Email == email && usr.Password == password).SingleOrDefault<User>();
                    if (user != null)
                    {
                        _userLogs.Where(log => log.UserId == user.Id).SingleOrDefault<UserLog>().LoggedInAt = System.DateTime.Now;
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveUserLogsAsync(_userLogs);
                }
            }
            return response;
        }
    }
}