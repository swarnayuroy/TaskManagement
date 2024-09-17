using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using API_Service.Data;
using API_Service.Repository.Interface;

namespace API_Service.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private static IList<Models.User> _users;
        private static IList<Models.Task> _tasks;
        private static IList<Models.UserTaskDetail> _taskDetails;
        private static IList<Models.TaskComment> _taskComments;
        public TaskRepository()
        {
            _users = SampleData.GetAllUser();
            _tasks = SampleData.GetAllTask();
            _taskDetails = SampleData.GetAllTaskDetail();
            _taskComments = SampleData.GetAllComment();
        }
        public async Task<DTO.UserTask> GetUserTask(Guid userId)
        {
            DTO.UserTask userTask = null;
            try
            {
                userTask = await Task.Run(() => {
                    List<Models.Task> tasks = null;
                    List<Models.TaskComment> taskComments = null;
                    var user = _users.Where(usr => usr.Id == userId).SingleOrDefault<Models.User>();
                    var taskIds = _taskDetails.Where(t => t.UserId == userId).Select(id => id.TaskId).ToList();
                    if (user != null)
                    {
                        if (taskIds.Count >= 1)
                        {
                            foreach (var taskId in taskIds)
                            {
                                var task = _tasks.Where(t => t.Id == taskId).SingleOrDefault<Models.Task>();
                                var comment = _taskComments.Where(c => c.TaskId == taskId).SingleOrDefault<Models.TaskComment>();
                                tasks.Add(task);
                                if (comment != null)
                                {
                                    taskComments.Add(comment);
                                }
                            }
                        }
                        return new DTO.UserTask() { User = user, Tasks = tasks, Comments = taskComments };
                    }
                    return null;
                });
            }
            catch (Exception)
            {
                throw;
            }
            return userTask;
        }
        public async Task<Models.Task> GetTask(Guid taskId)
        {
            Models.Task task = null;
            try
            {
                task = await System.Threading.Tasks.Task.Run(() =>
                {
                    return _tasks.Where(t => t.Id == taskId)
                                 .SingleOrDefault<Models.Task>();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return task;
        }
        public async Task<bool> AddTask(Guid userId, Models.Task task)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    task.Id = Guid.NewGuid();
                    task.CreatedAt = System.DateTime.Now;
                    _tasks.Add(task);
                    _taskDetails.Add(
                        new Models.UserTaskDetail()
                        {
                            UserId = userId,
                            TaskId = task.Id
                        }
                    );
                    return true;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveTasksAsync(_tasks);
                    await SampleData.SaveTaskDetailsAsync(_taskDetails);
                }
            }
            return response;
        }
        public async Task<bool> EditTask(Guid taskId, Models.Task task)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    var usrTask = _tasks.Where(t => t.Id == taskId)
                                        .SingleOrDefault<Models.Task>();
                    if (usrTask != null)
                    {
                        _tasks[_tasks.IndexOf(usrTask)] = task;
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveTasksAsync(_tasks);
                }
            }
            return response;
        }
        public async Task<bool> RemoveTask(Guid userId, Guid taskId)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {

                    var taskDetail = _taskDetails.Where(task => task.UserId == userId && task.TaskId == taskId)
                                                 .SingleOrDefault<Models.UserTaskDetail>();

                    if (taskDetail != null)
                    {
                        var usrTask = _tasks.Where(task => task.Id == taskId)
                                            .SingleOrDefault<Models.Task>();
                        var taskComments = _taskComments.Where(comment => comment.TaskId == taskId)
                                                        .ToList<Models.TaskComment>();
                        if (taskComments.Count >= 1)
                        {
                            foreach (var comment in taskComments)
                            {
                                _taskComments.Remove(comment);
                            }
                        }
                        _taskDetails.Remove(taskDetail);
                        _tasks.Remove(usrTask);
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveTaskCommentsAsync(_taskComments);
                    await SampleData.SaveTasksAsync(_tasks);
                    await SampleData.SaveTaskDetailsAsync(_taskDetails);
                }
            }
            return response;
        }
        public async Task<bool> AddComment(Guid userId, Guid taskId, Models.TaskComment comment)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    comment.Id = Guid.NewGuid();
                    comment.TaskId = taskId;
                    comment.UserId = userId;
                    comment.UserName = _users.Where(usr => usr.Id == userId).Select(usr => usr.FirstName).ToString() + " " + _users.Where(usr => usr.Id == userId).Select(usr => usr.LastName).ToString();
                    comment.CommentedAt = System.DateTime.Now;
                    _taskComments.Add(comment);
                    return true;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveTaskCommentsAsync(_taskComments);
                }
            }
            return response;
        }
        public async Task<bool> EditComment(Guid commentId, Guid userId, Guid taskId, Models.TaskComment comment)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    var userComment = _taskComments.Where(c => c.Id == commentId && c.UserId == userId && c.TaskId == taskId)
                                                   .SingleOrDefault<Models.TaskComment>();
                    if (userComment != null)
                    {
                        comment.EditedAt = System.DateTime.Now;
                        _taskComments[_taskComments.IndexOf(userComment)] = comment;
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveTaskCommentsAsync(_taskComments);
                }
            }
            return response;
        }
        public async Task<bool> RemoveComment(Guid commentId, Guid userId, Guid taskId)
        {
            bool response = false;
            try
            {
                response = await System.Threading.Tasks.Task.Run(() =>
                {
                    var userComment = _taskComments.Where(c => c.Id == commentId && c.UserId == userId && c.TaskId == taskId)
                                                   .SingleOrDefault<Models.TaskComment>();
                    if (userComment != null)
                    {
                        _taskComments.Remove(userComment);
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (response)
                {
                    await SampleData.SaveTaskCommentsAsync(_taskComments);
                }
            }
            return response;
        }
    }
}