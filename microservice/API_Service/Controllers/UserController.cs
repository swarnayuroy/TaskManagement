using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using API_Service.Repository.Interface;
using log4net;

namespace API_Service.Controllers
{
    public class UserController : ApiController
    {
        private readonly ILog _logger;
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            this._logger = LogManager.GetLogger(typeof(UserController));
            this._repository = repository;
        }

        [HttpGet]
        [Route("api/user/get/{id}")]
        public async Task<HttpResponseMessage> Getuser(string id)
        {
            try
            {
                Models.User user = await _repository.GetUser(Guid.Parse(id));
                return user != null ? Request.CreateResponse(HttpStatusCode.OK, user) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
