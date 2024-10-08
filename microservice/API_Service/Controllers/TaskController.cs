﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Service.DTO;
using API_Service.Models;
using API_Service.Models.ViewModel;
using API_Service.Utils.Mapper;
using API_Service.Repository.Interface;
using System.Threading.Tasks;
using log4net;

namespace API_Service.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ILog _logger;
        private readonly ITaskRepository _repository;
        private readonly IMapperService _mapper;
        public TaskController(ITaskRepository repository, IMapperService mapper)
        {
            this._logger = LogManager.GetLogger(typeof(TaskController));
            this._repository = repository;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("api/getusertask/{userId}")]
        public async Task<HttpResponseMessage> GetUserTask(Guid userId)
        {
            try
            {                
                UserTaskDTO userTask = _mapper.Map<UserTask, UserTaskDTO>(await _repository.GetUserTask(userId));
                return userTask != null ? Request.CreateResponse(HttpStatusCode.OK, userTask) : Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/task/get/{id}")]
        public async Task<HttpResponseMessage> GetTask(Guid id)
        {
            try
            {
                Models.Task task = await _repository.GetTask(id);
                return task != null ? Request.CreateResponse(HttpStatusCode.OK, task) : Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/task/create")]
        public async Task<HttpResponseMessage> CreateTask(Guid userId, [FromBody] TaskDTO task)
        {
            try
            {
                bool response = await _repository.AddTask(userId, _mapper.Map<TaskDTO, Models.Task>(task));
                return response ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut]
        [Route("api/task/edit")]
        public async Task<HttpResponseMessage> EditTask(Guid taskId, [FromBody] TaskDTO task)
        {
            try
            {
                bool response = await _repository.EditTask(taskId, _mapper.Map<TaskDTO, Models.Task>(task));
                return response ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/task/delete")]
        public async Task<HttpResponseMessage> DeleteTask(Guid userId, Guid taskId)
        {
            try
            {
                bool response = await _repository.RemoveTask(userId, taskId);
                return response ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Route("api/task/comment/add")]
        public async Task<HttpResponseMessage> AddTaskComment(Guid userId, Guid taskId, [FromBody] TaskCommentDTO comment)
        {
            try
            {
                bool response = await _repository.AddComment(userId, taskId, _mapper.Map<TaskCommentDTO, TaskComment>(comment));
                return response ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut]
        [Route("api/task/comment/edit")]
        public async Task<HttpResponseMessage> EditComment(Guid commentId, Guid userId, Guid taskId, [FromBody] TaskCommentDTO comment)
        {
            try
            {
                bool response = await _repository.EditComment(commentId, userId, taskId, _mapper.Map<TaskCommentDTO, TaskComment>(comment));
                return response ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/task/comment/remove")]
        public async Task<HttpResponseMessage> RemoveComment(Guid commentId, Guid userId, Guid taskId)
        {
            try
            {
                bool response = await _repository.RemoveComment(commentId, userId, taskId);
                return response ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
