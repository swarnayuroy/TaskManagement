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
using client.Utils.CustomException;
using client.Utils.Filter;

namespace client.Controllers
{
    [Restrict]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*", Location = OutputCacheLocation.None)]
    public class HomeController : Controller
    {
        private readonly IDataAccessLayer _dataAccess;
        private Logger<HomeController> _logger;

        public HomeController(IDataAccessLayer dataAccess)
        {
            this._dataAccess = dataAccess;
            this._logger = new Logger<HomeController>();
        }
        
        public async Task<ActionResult> TaskBoard()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var userDataResponse = new ServiceResponse();
            var userDetail = new UserDetail();

            try
            {
                string sessionToken = Request.Cookies["sessionToken"]?.Value;
                if (!String.IsNullOrEmpty(sessionToken))
                {
                    claimsPrincipal = JwtHelper.DecodeToken(sessionToken);
                    string userId = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                    if (!String.IsNullOrEmpty(userId))
                    {
                        userDataResponse = await _dataAccess.GetUserDetail(sessionToken, userId);
                        userDetail = FilterResponse<UserDetail>.GetData(userDataResponse);

                        return View(new UserSessionDetail
                        {
                            AvatarText = userDetail.Name,
                            UserDetail = userDetail,
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
            catch (UnauthorizedException ex)
            {
                _logger.LogDetails(LogType.WARNING, $"{ex.Message}");
                return RedirectToAction("Logout");
            }
            catch (Exception ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
                return View(new UserSessionDetail
                {
                    AvatarText = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                    UserDetail = userDetail,
                    ToastNotification = new ToastNotification
                    {
                        IsEnable = true,
                        Type = userDataResponse.StatusCode,
                        StatusIcon = ToastNotification.WARNING_ICON,
                        Message = userDataResponse.Message
                    }
                });
            }
        }

        public ActionResult Logout()
        {
            try
            {
                var cookie = Request.Cookies["sessionToken"];
                if (cookie != null)
                {
                    var claimsPrincipal = JwtHelper.DecodeToken(cookie.Value);
                    string userId = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                    cookie.Expires = DateTime.Now.AddMinutes(-1);
                    cookie.Value = String.Empty;
                    Response.Cookies.Add(cookie);
                    Request.Cookies.Remove("sessionToken");

                    Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();

                    _logger.LogDetails(LogType.INFO, $"{userId} logged out");
                }
                
                return RedirectToAction("SignIn", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogDetails(LogType.ERROR, ex.Message);
                return RedirectToAction("SignIn", "Account");
            }
            
        }
    }
}