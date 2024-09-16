using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.Models
{
    public class TaskComment
    {
        private Guid _id;
        private Guid _taskId;
        private Guid _userId;
        private string _userName;
        private string _comment;
        private DateTime _commentedAt;
        private DateTime _editedAt;
        private DateTime _deletedAt;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != Guid.Empty)
                {
                    _id = value;
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
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _userName = value;
                }
            }
        }
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _comment = value;
                }
            }
        }
        public DateTime CommentedAt
        {
            get
            {
                return _commentedAt;
            }
            set
            {
                if (value != null)
                {
                    _commentedAt = value;
                }
            }
        }
        public DateTime EditedAt
        {
            get
            {
                return _editedAt;
            }
            set
            {
                if (_editedAt != null)
                {
                    _editedAt = value;
                }
            }
        }
        public DateTime DeletedAt
        {
            get
            {
                return _deletedAt;
            }
            set
            {
                if (value != null)
                {
                    _deletedAt = value;
                }
            }
        }
    }
}