﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}