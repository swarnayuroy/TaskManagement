using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace client.Models
{
    public class ServiceResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Status { get; set; }      
        public string Message { get; set; }
    }
    public class ServiceDataResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }
}