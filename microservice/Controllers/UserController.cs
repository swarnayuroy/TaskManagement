using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using microservice.DTO;
using microservice.Service;

namespace microservice.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            this._accountService = accountService;
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
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user records found!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
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
                    return Request.CreateResponse(HttpStatusCode.OK, new UserDTO
                    {
                        Id = userDetail.Id,
                        Name = userDetail.Name,
                        Email = userDetail.Email,
                        IsVerfied = userDetail.IsVerfied,
                        Password = String.Empty
                    });
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
