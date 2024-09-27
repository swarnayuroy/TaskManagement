using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.Models.ViewModel
{
    public class UserTask
    {
        public User User { get; set; }
        public IList<Task> Tasks { get; set; }
        public IList<TaskComment> Comments { get; set; }
    }
}