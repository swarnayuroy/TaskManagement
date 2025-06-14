using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microservice.Utils.CustomException
{
    public class AccountServiceException : Exception
    {
        public AccountServiceException(string message) : base(message) { }
    }
}