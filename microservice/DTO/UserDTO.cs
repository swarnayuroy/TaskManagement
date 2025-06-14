using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microservice.DTO
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsVerfied { get; set; }
    }
}