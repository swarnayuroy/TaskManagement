using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_Service.Models;

namespace API_Service.Data
{
    public class SampleData
    {
        public static IList<User> users = new List<User>() {
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
            }
        };
        public static IList<UserLog> userLogs = new List<UserLog>();
        public static IList<Task> tasks = new List<Task>();
        public static IList<UserTaskDetail> taskDetails = new List<UserTaskDetail>();
        public static IList<TaskComment> taskComments = new List<TaskComment>();
        public static async System.Threading.Tasks.Task SaveUsersAsync(IList<User> usersData)
        {
            await System.Threading.Tasks.Task.Run(() => users = usersData);
        }
        public static async System.Threading.Tasks.Task SaveUserLogsAsync(IList<UserLog> userLogsData)
        {
            await System.Threading.Tasks.Task.Run(() => userLogs = userLogsData);
        }
        public static async System.Threading.Tasks.Task SaveTasksAsync(IList<Task> tasksData)
        {
            await System.Threading.Tasks.Task.Run(() => tasks = tasksData);
        }
        public static async System.Threading.Tasks.Task SaveTaskDetailsAsync(IList<UserTaskDetail> taskDetailsData)
        {
            await System.Threading.Tasks.Task.Run(() => taskDetails = taskDetailsData);
        }
        public static async System.Threading.Tasks.Task SaveTaskCommentsAsync(IList<TaskComment> commentsData)
        {
            await System.Threading.Tasks.Task.Run(() => taskComments = commentsData);
        }
    }
}