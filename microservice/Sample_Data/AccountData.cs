﻿using microservice.Models;
using microservice.Sample_Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace microservice.Sample_Data
{
    public class AccountData : IAccountContext
    {
        #region Sample Data
        private static IList<User> _userList = new List<User>()
        {
            new User
            {
                Id = Guid.Parse("1e61f4a4-0e98-4fd9-bfc4-0c1c0da4a66e"),
                Name = "John Doe",
                Email = "doe.john@gmail.com",
                IsVerfied = false
            },
            new User
            {
                Id = Guid.Parse("4b79aeeb-96cd-49bf-abf0-8b5f6f693467"),
                Name = "Jane Doe",
                Email = "doe.jane@gmail.com",
                IsVerfied = true
            }
        };

        private static IList<Account> _accountsDetail = new List<Account>()
        {
            new Account
            {
                Id = Guid.Parse("48e283eb-8193-4de0-a025-e8dcb6bc678a"),
                UserId = Guid.Parse("1e61f4a4-0e98-4fd9-bfc4-0c1c0da4a66e"),
                Password = "TestJohn@1994",
                CreatedAt = DateTime.Parse("2025-04-10T10:15:30")
            },
            new Account
            {
                Id = Guid.Parse("50b26a44-c7f9-462a-9d4f-c66ac2e9938e"),
                UserId = Guid.Parse("4b79aeeb-96cd-49bf-abf0-8b5f6f693467"),
                Password = "TestJane@1994",
                CreatedAt = DateTime.Parse("2025-05-10T10:15:30")
            }
        };
        #endregion
        
        #region GetAccountData
        public IList<User> User { get { return _userList; } }

        public IList<Account> Account { get { return _accountsDetail; } }
        #endregion

        #region SaveAccountData
        public async Task SaveAccountAsync(IList<Account> accountList)
        {
            await Task.Run(() => _accountsDetail = accountList);
        }

        public async Task SaveUserAsync(IList<User> userList)
        {
            await Task.Run(() => _userList = userList);
        }
        #endregion
    }
}