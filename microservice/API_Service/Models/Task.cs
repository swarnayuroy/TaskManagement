using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.Models
{
    public class Task
    {
        private Guid _id;
        private string _title;
        private string _description;
        private int _type;
        private int _status;
        private DateTime _createdAt;
        private DateTime _startDate;
        private DateTime _endDate;
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
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _title = value;
                }
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _description = value;
                }
            }
        }
        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value == 0 || value == 1)
                {
                    _type = value;
                }
            }
        }
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value >= 0 && value <= 3)
                {
                    _status = value;
                }
            }
        }
        public DateTime CreatedAt
        {
            get
            {
                return _createdAt;
            }
            set
            {
                if (value != null)
                {
                    _createdAt = value;
                }
            }
        }
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value != null)
                {
                    _startDate = value;
                }
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value != null)
                {
                    _endDate = value;
                }
            }
        }
    }
}