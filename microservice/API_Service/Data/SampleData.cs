using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_Service.Models;

namespace API_Service.Data
{
    public class SampleData
    {
        private static IList<User> _users = new List<User>() {
            new User
            {
                Id = Guid.Parse("6f31fe8e-fd4b-4d0e-bd37-6526e38eef1e"),
                FirstName = "Mohan",
                LastName = "Raina",
                Email = "raina.mohan@gmail.com",
                Password = "Test1@1234"
            },
            new User
            {
                Id = Guid.Parse("a1a89b25-c08b-4195-af32-9d167e4d6947"),
                FirstName = "Avijeet",
                LastName = "Sinha",
                Email = "avijeet.s@yahoo.com",
                Password = "Test2@1234"
            },
            new User
            {
                Id = Guid.Parse("83248ffb-5dff-4da3-8552-8a85ac76590b"),
                FirstName = "Srinivas",
                LastName = "Shetty",
                Email = "s.rinivas@yahoo.com",
                Password = "Test3@1234"
            }
        };
        private static IList<UserLog> _userLogs = new List<UserLog>() {
            new UserLog
            {
                UserId = Guid.Parse("6f31fe8e-fd4b-4d0e-bd37-6526e38eef1e"),
                RegisteredAt = Convert.ToDateTime("2024-09-16")
            },
            new UserLog
            {
                UserId = Guid.Parse("a1a89b25-c08b-4195-af32-9d167e4d6947"),
                RegisteredAt = Convert.ToDateTime("2024-09-16")
            },
            new UserLog
            {
                UserId = Guid.Parse("83248ffb-5dff-4da3-8552-8a85ac76590b"),
                RegisteredAt = Convert.ToDateTime("2024-09-17")
            }
        };
        private static IList<Task> _tasks = new List<Task>();
        private static IList<UserTaskDetail> _taskDetails = new List<UserTaskDetail>();
        private static IList<TaskComment> _taskComments = new List<TaskComment>();
        public static IList<User> GetAllUser()
        {            
            return _users;
        }
        public static IList<UserLog> GetUserLogs()
        {
            return _userLogs;
        }
        public static IList<Task> GetAllTask()
        {
            return _tasks;
        }
        public static IList<UserTaskDetail> GetAllTaskDetail()
        {
            return _taskDetails;
        }
        public static IList<TaskComment> GetAllComment()
        {
            return _taskComments;
        }
        public static async System.Threading.Tasks.Task SaveUsersAsync(IList<User> usersData)
        {
            await System.Threading.Tasks.Task.Run(() => _users = usersData);
        }
        public static async System.Threading.Tasks.Task SaveUserLogsAsync(IList<UserLog> userLogsData)
        {
            await System.Threading.Tasks.Task.Run(() => _userLogs = userLogsData);
        }
        public static async System.Threading.Tasks.Task SaveTasksAsync(IList<Task> tasksData)
        {
            await System.Threading.Tasks.Task.Run(() => _tasks = tasksData);
        }
        public static async System.Threading.Tasks.Task SaveTaskDetailsAsync(IList<UserTaskDetail> taskDetailsData)
        {
            await System.Threading.Tasks.Task.Run(() => _taskDetails = taskDetailsData);
        }
        public static async System.Threading.Tasks.Task SaveTaskCommentsAsync(IList<TaskComment> commentsData)
        {
            await System.Threading.Tasks.Task.Run(() => _taskComments = commentsData);
        }
    }
}