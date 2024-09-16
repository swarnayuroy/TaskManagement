using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Service.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<bool> AddTask(Guid userId, Models.Task task);
        Task<DTO.UserTask> GetUserTask(Guid userId);
        Task<Models.Task> GetTask(Guid taskId);
        Task<bool> EditTask(Guid taskId, Models.Task task);
        Task<bool> RemoveTask(Guid userId, Guid taskId);
        Task<bool> AddComment(Guid userId, Guid taskId, Models.TaskComment comment);
        Task<bool> EditComment(Guid commentId, Guid userId, Guid taskId, Models.TaskComment comment);
        Task<bool> RemoveComment(Guid commentId, Guid userId, Guid taskId);
    }
}
