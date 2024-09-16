using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.Models
{
    public class UserLog
    {
        private Guid _userId;
        private DateTime _registeredAt;
        private DateTime _loggedInAt;
        public Guid UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (!value.Equals(Guid.Empty))
                {
                    _userId = value;
                }
            }
        }
        public DateTime RegisteredAt
        {
            get
            {
                return _registeredAt;
            }
            set
            {
                if (value != null)
                {
                    _registeredAt = value;
                }
            }
        }
        public DateTime LoggedInAt
        {
            get
            {
                return _loggedInAt;
            }
            set
            {
                if (value != null)
                {
                    _loggedInAt = value;
                }
            }
        }
    }
}