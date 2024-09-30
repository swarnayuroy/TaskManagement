using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Service.Models;
using API_Service.Models.ViewModel;
using API_Service.DTO;
using API_Service.Utils.Mapper;
using API_Service.Repository.Interface;
using System.Threading.Tasks;
using log4net;

namespace API_Service.Controllers
{
    public class AccountController : ApiController
    {
        private readonly ILog _logger;
        private readonly IUserRepository _repository;
        private readonly IMapperService _mapper;
        public AccountController(IUserRepository repository, IMapperService mapper)
        {
            this._logger = LogManager.GetLogger(typeof(AccountController));
            this._repository = repository;
            this._mapper = mapper;
        }
        
        [HttpGet]
        [Route("api/checkcredential")]
        public async Task<HttpResponseMessage> CheckCredentialIsValid([FromBody] UserCredentialDTO userCredential)
        {
            try
            {
                UserCredential credential = _mapper.Map<UserCredentialDTO, UserCredential>(userCredential);
                bool response = await _repository.CheckValidUser(credential.Email, credential.Password);
                return response ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Route("api/registeruser")]
        public async Task<HttpResponseMessage> RegisterUser([FromBody] User user)
        {
            try
            {
                bool response = await _repository.AddUser(user);
                return response ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
    }
}
