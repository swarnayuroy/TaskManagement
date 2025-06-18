using client.Models;
using client.Utils.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace client.Utils.Filter
{
    public class FilterResponse<T>
    {
        public static T GetData(ServiceResponse response)
        {
            if (response.Status)
            {
                return (response as ServiceDataResponse<T>).Data;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException($"Unauthorized session");
            }
            else
            {
                throw new Exception($"{response.Message}");
            }
        }
    }
}