using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microservice.Utils
{
    public class ResponseDetail
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ResponseDataDetail<T> : ResponseDetail
    {
        public T Data { get; set; }
    }
}