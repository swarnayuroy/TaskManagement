using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using microservice.DTO;
using microservice.Service;
using microservice.Utils;

namespace microservice.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IAccountService _accountService;
        private Logger<UserController> _logger;
        public UserController(IAccountService accountService)
        {
            this._accountService = accountService;
            this._logger = new Logger<UserController>();
        }

        [HttpGet]
        [Route("api/user/get")]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var users = await _accountService.Get();
                if (users != null)
                {
                    _logger.LogDetails(LogType.INFO, $"{users.Count} users found");
                    IList<UserDTO> userList = new List<UserDTO>();
                    foreach (var user in users)
                    {
                        userList.Add(new UserDTO { 
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            IsVerfied = user.IsVerfied,
                            Password = string.Empty
                        });
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, userList);
                }

                _logger.LogDetails(LogType.WARNING, $"No users records");
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user records found!");
            }
            catch (Exception ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }

        [HttpGet]
        [Route("api/user/get/{id}")]
        public async Task<HttpResponseMessage> GetById(string id)
        {
            try
            {
                var userDetail = await _accountService.GetById(Guid.Parse(id));
                if (userDetail != null)
                {
                    _logger.LogDetails(LogType.INFO, $"Found user with id:{id}");
                    return Request.CreateResponse(HttpStatusCode.OK, new UserDTO
                    {
                        Id = userDetail.Id,
                        Name = userDetail.Name,
                        Email = userDetail.Email,
                        IsVerfied = userDetail.IsVerfied,
                        Password = String.Empty
                    });
                }

                _logger.LogDetails(LogType.WARNING, $"No user found with id:{id}");
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found!");
            }
            catch (Exception ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }
    }
}
