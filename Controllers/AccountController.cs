using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Forum.Models;
using Forum.Context;
using Forum.Repo;

namespace Forum.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        Repository Repository = new Repository();
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetUserInfo()
        { 
            if (ViewBag.IsAuth = User.Identity.IsAuthenticated)
            {
                var UserName = Repository.CurrentUser.Name;
                var Role = (User.IsInRole("Admin")) ? "администратор" : "пользователь";
                return Json(new { isAuth = true, username = UserName, duty = Role });
            }
                return Json(new { isAuth = false });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Login, model.Password))
                {
                    var user = Repository.Users.FirstOrDefault(u => u.Login == model.Login);
                    if (user != null)
                    {
                        user.LastVisit = DateTime.Now;
                        Repository.UpdateUser(user);
                    }
                    FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                var user = Membership.CreateUser(model.Login, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                
                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    Roles.AddUserToRole(model.Login, "User");
                    Repository.CreateUser(new User() { Login = model.Login, Name = model.Login });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public void ChangePassword(String UserName, String oldPassword, String newPassword)
        {
            var user = Membership.GetUser(UserName);
            user.ChangePassword(oldPassword, newPassword);
        }
    }
}