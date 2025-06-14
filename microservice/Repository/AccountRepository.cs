using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using microservice.DTO;
using microservice.Models;
using microservice.Sample_Data.Context;
using microservice.Service;
using microservice.Utils;
using microservice.Utils.AuthenticationService;
using microservice.Utils.CustomException;

namespace microservice.Repository
{
    public class AccountRepository : IAccountService
    {
        private readonly IAccountContext _context;
        public AccountRepository(IAccountContext accountContext)
        {
            this._context = accountContext;
        }

        #region User Service
        public async Task<IList<User>> Get()
        {
            try
            {
                var users = await Task.Run(() => {
                    return _context.User;
                });
                return users != null ? users : null;
            }
            catch (Exception)
            {
                throw new AccountServiceException("Failed to retrieve all users!");
            }
        }
        public async Task<User> GetById(Guid id)
        {
            try
            {
                var userDetail = await Task.Run(() => {
                    return _context.User.Where(user => user.Id == id).SingleOrDefault<User>();
                });
                return userDetail != null ? userDetail : null;
            }
            catch (Exception)
            {
                throw new AccountServiceException("Failed to find the user!");
            }
            
        }
        #endregion

        #region Account Service
        public async Task<ResponseDetail> Check(string email, string password)
        {
            try
            {
                Guid userId = IsValidAccount(email);
                if (userId != Guid.Empty)
                {
                    var userAccount = _context.Account
                    .Where(account => account.UserId == userId && account.Password == password)
                    .SingleOrDefault<Account>();

                    if (userAccount != null)
                    {
                        var userDetail = await GetById(userId);
                        string userToken = Authentication.GenerateToken(userDetail);

                        if (!String.IsNullOrEmpty(userToken))
                        {
                            await SetAccountLoginTime(userAccount);

                            return new ResponseDataDetail<string>
                            {
                                Status = true,
                                Message = "Account has been validated successfully",
                                Data = userToken
                            };
                        }                        
                    }
                    return new ResponseDetail { Status = false, Message = "Incorrect Password" };
                }
                return new ResponseDetail { Status = false, Message = "Incorrect Email" };
            }
            catch (Exception)
            {
                throw new AccountServiceException("Some error occurred");
            }
        }        
        public async Task<ResponseDetail> Register(UserDTO userDetails)
        {
            try
            {
                Guid isValidUserId = IsValidAccount(userDetails.Email);
                if (isValidUserId == Guid.Empty)
                {
                    IList<User> users = _context.User;
                    IList<Account> accounts = _context.Account;

                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = userDetails.Name,
                        Email = userDetails.Email,
                        IsVerfied = false
                    };

                    users.Add(newUser);
                    accounts.Add(new Account
                    {
                        Id = Guid.NewGuid(),
                        UserId = newUser.Id,
                        Password = userDetails.Password,
                        CreatedAt = DateTime.Now
                    });

                    await Task.WhenAll(
                        _context.SaveUserAsync(users),
                        _context.SaveAccountAsync(accounts)
                        );
                    return new ResponseDetail { Status = true, Message = "Account created successfully" };
                }
                return new ResponseDetail { Status = false, Message = "This email already exists" };
            }
            catch (Exception)
            {
                throw new AccountServiceException("Failed to create your account!");
            }            
        }
        #endregion
        
        public async Task SetAccountLoginTime(Account userAccount)
        {
            userAccount.LoggedInAt = DateTime.Now;

            var accounts = _context.Account;
            accounts[accounts.IndexOf(userAccount)] = userAccount;
            await _context.SaveAccountAsync(accounts);
        }
        public Guid IsValidAccount(string email)
        {
            User userDetail = _context.User.Where(user => user.Email == email)
                        .SingleOrDefault<User>();

            if (userDetail != null)
            {
                var accounDetail = _context.Account.Where(account =>
                        account.UserId == userDetail.Id).SingleOrDefault<Account>();

                return accounDetail != null ? accounDetail.UserId : Guid.Empty;
            }
            return Guid.Empty;            
        }
    }
}