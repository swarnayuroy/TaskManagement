using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.Models
{
    public class UserTaskDetail
    {
        private Guid _userId;
        private Guid _taskId;
        public Guid UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (value != Guid.Empty)
                {
                    _userId = value;
                }
            }
        }
        public Guid TaskId
        {
            get
            {
                return _taskId;
            }
            set
            {
                if (value != Guid.Empty)
                {
                    _taskId = value;
                }
            }
        }
    }
}