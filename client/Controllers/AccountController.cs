using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using client.DataLayer;
using client.Models;
using client.Utils;

namespace client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDataAccessLayer _dataAccess;
        private Logger<AccountController> _logger;
        public AccountController(IDataAccessLayer dataAccess)
        {
            this._dataAccess = dataAccess;
            this._logger = new Logger<AccountController>();
        }
        
        // GET: SignIn
        public ActionResult SignIn()
        {
            return View(new Form
                {
                    showSignInForm = true,
                    showSignUpForm = false,
                    ToastNotification = new ToastNotification
                    {
                        IsEnable = false,
                    }
                }
            );
        }

        // GET: SignUp
        public ActionResult SignUp()
        {
            //will be redirecting to "SignIn" action while enabling the registration form
            return View("SignIn", new Form
                {
                    showSignInForm = false,
                    showSignUpForm = true,
                    ToastNotification = new ToastNotification
                    {
                        IsEnable = false,
                    }
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(Form formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(new Form
                {
                    SignIn = formModel.SignIn,
                    showSignInForm = true,
                    showSignUpForm = false,
                    ToastNotification = new ToastNotification
                    {
                        IsEnable = false,
                    }
                });
            }
            var response = await _dataAccess.CheckCredential(formModel.SignIn);
            if (response.Status)
            {
                string sessionToken = response.Message;
                var cookie = new HttpCookie("sessionToken", sessionToken) 
                {
                    HttpOnly = true,
                    Secure = true
                };
                Response.Cookies.Add(cookie);
                _logger.LogDetails(LogType.INFO, $"{formModel.SignIn.Email} signed in successfully.");

                //will be redirecting to "Home/TaskBoard"
                return RedirectToAction("TaskBoard", "Home");
            }

            _logger.LogDetails(LogType.WARNING, response.Message);

            ModelState.Clear();
            return View("SignIn", new Form {
                showSignInForm = true,
                showSignUpForm = false,
                ToastNotification = new ToastNotification
                {
                    IsEnable = true,
                    Type = response.StatusCode,
                    StatusIcon = ToastNotification.WARNING_ICON,
                    Message = response.Message
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(Form formModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SignIn", new Form
                {
                    SignUp = formModel.SignUp,
                    showSignInForm = false,
                    showSignUpForm = true,
                    ToastNotification = new ToastNotification
                    {
                        IsEnable = false,
                    }
                });
            }
            var response = await _dataAccess.RegisterUser(formModel.SignUp);
            if (response.Status)
            {
                _logger.LogDetails(LogType.INFO, response.Message);

                ModelState.Clear();
                return View("SignIn", new Form
                {
                    showSignInForm = true,
                    showSignUpForm = false,
                    ToastNotification = new ToastNotification
                    {
                        IsEnable = true,
                        Type = response.StatusCode,
                        StatusIcon = ToastNotification.SUCCESS_ICON,
                        Message = response.Message
                    }
                });
            }

            _logger.LogDetails(LogType.WARNING, response.Message);

            return View("SignIn", new Form
            {
                SignUp = formModel.SignUp,
                showSignInForm = false,
                showSignUpForm = true,
                ToastNotification = new ToastNotification
                {
                    IsEnable = true,
                    Type = response.StatusCode,
                    StatusIcon = ToastNotification.WARNING_ICON,
                    Message = response.Message
                }
            });
        }
    }
}