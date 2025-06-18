using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace client.Utils.CustomException
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}