using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using microservice.Service;
using microservice.DTO;
using microservice.Utils;

namespace microservice.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        
        [HttpPost]
        [Route("api/account/check")]
        public async Task<HttpResponseMessage> Check([FromBody] CredentialDTO credentialDetail)
        {
            try
            {
                var serviceResponse = await _accountService.Check(credentialDetail.Email, credentialDetail.Password);
                return serviceResponse.Status ? Request.CreateResponse(HttpStatusCode.OK, (serviceResponse as ResponseDataDetail<string>).Data)
                    : Request.CreateErrorResponse(HttpStatusCode.Forbidden, serviceResponse.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }            
        }

        [HttpPost]
        [Route("api/account/register")]
        public async Task<HttpResponseMessage> Register([FromBody] UserDTO dtoDetails)
        {
            try
            {
                var serviceResponse = await _accountService.Register(dtoDetails);
                return serviceResponse.Status ? Request.CreateResponse(HttpStatusCode.Created, serviceResponse.Message)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceResponse.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
