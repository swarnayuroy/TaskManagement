using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using client.DataLayer;
using client.Models;
using client.Utils;
using client.Utils.Filter;

namespace client.Controllers
{
    [Restrict]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*", Location = OutputCacheLocation.None)]
    public class HomeController : Controller
    {
        private readonly IDataAccessLayer _dataAccess;
        private UserDetail _userDetail;
        private ServiceResponse _userDataResponse;
        public HomeController(IDataAccessLayer dataAccess)
        {
            this._dataAccess = dataAccess;
        }
        
        public async Task<ActionResult> TaskBoard()
        {
            string sessionToken = Request.Cookies["sessionToken"]?.Value;
            if (!String.IsNullOrEmpty(sessionToken))
            {
                var claimsPrincipal = JwtHelper.DecodeToken(sessionToken);
                string userId = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (!String.IsNullOrEmpty(userId))
                {
                    if (_userDetail == null)
                    {
                        _userDataResponse = await _dataAccess.GetUserDetail(sessionToken, userId);
                        if (_userDataResponse.Status)
                        {
                            _userDetail = (_userDataResponse as ServiceDataResponse<UserDetail>)?.Data;
                        }
                        else if (_userDataResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            return RedirectToAction("Logout");
                        }
                        else
                        {
                            return View(new UserSessionDetail
                            {
                                ToastNotification = new ToastNotification
                                {
                                    IsEnable = true,
                                    Type = _userDataResponse.StatusCode,
                                    StatusIcon = ToastNotification.WARNING_ICON,
                                    Message = _userDataResponse.Message
                                }
                            });
                        }                        
                    }
                    
                    return View(new UserSessionDetail
                    {
                        AvatarText = _userDetail.Name,
                        UserDetail = _userDetail,
                        ToastNotification = new ToastNotification
                        {
                            IsEnable = false,                            
                        }
                    });
                }

                return RedirectToAction("Logout");
            }
            return RedirectToAction("SignIn", "Account");
        }
        public ActionResult Logout()
        {
            var cookie = Request.Cookies["sessionToken"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddMinutes(-1);
                cookie.Value = String.Empty;
                Response.Cookies.Add(cookie);
                Request.Cookies.Remove("sessionToken");

                Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
            }
            return RedirectToAction("SignIn", "Account");
        }
    }
}