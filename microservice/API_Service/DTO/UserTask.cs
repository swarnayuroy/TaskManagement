using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_Service.Models;

namespace API_Service.DTO
{
    public class UserTask
    {
        public User User { get; set; }
        public IList<Task> Tasks { get; set; }
        public IList<TaskComment> Comments { get; set; }
    }
}