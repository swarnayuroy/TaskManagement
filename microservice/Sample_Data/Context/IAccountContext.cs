using microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Sample_Data.Context
{
    public interface IAccountContext
    {
        IList<User> User { get; }
        IList<Account> Account { get; }
        Task SaveUserAsync(IList<User> userList);
        Task SaveAccountAsync(IList<Account> accountList);
    }
}
