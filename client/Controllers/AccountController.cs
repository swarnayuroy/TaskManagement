using client.DataLayer;
using client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDataAccessLayer _dataAccess;
        public AccountController(IDataAccessLayer dataAccess)
        {
            this._dataAccess = dataAccess;
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

                //will be redirecting to "Home/TaskBoard"
                return RedirectToAction("TaskBoard", "Home");
            }
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