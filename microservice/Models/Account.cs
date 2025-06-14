using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace microservice.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Password { get; set; }

        /*
         sets date and time of new registration
        */
        public DateTime CreatedAt { get; set; }

        /*
         sets date an time of an user sign-in
        */
        public DateTime LoggedInAt { get; set; }
    }
}