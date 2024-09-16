using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.Models
{
    public class User
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (!value.Equals(Guid.Empty))
                {
                    _id = value;
                }
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _firstName = value;
                }
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _lastName = value;
                }
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _email = value;
                }
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _password = value;
                }
            }
        }
    }
}