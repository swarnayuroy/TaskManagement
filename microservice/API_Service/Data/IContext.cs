using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API_Service.Models;

namespace API_Service.Data
{
    public interface IContext
    {
        #region GetContext
        System.Threading.Tasks.Task<IList<User>> GetAllUser();
        System.Threading.Tasks.Task<IList<UserLog>> GetUserLogs();
        System.Threading.Tasks.Task<IList<Task>> GetAllTask();
        System.Threading.Tasks.Task<IList<UserTaskDetail>> GetAllTaskDetail();
        System.Threading.Tasks.Task<IList<TaskComment>> GetAllComment();
        #endregion

        #region SaveContext
        System.Threading.Tasks.Task SaveUsersAsync(IList<User> usersData);
        System.Threading.Tasks.Task SaveUserLogsAsync(IList<UserLog> userLogsData);
        System.Threading.Tasks.Task SaveTasksAsync(IList<Task> tasksData);
        System.Threading.Tasks.Task SaveTaskDetailsAsync(IList<UserTaskDetail> taskDetailsData);
        System.Threading.Tasks.Task SaveTaskCommentsAsync(IList<TaskComment> commentsData);
        #endregion
    }
}
