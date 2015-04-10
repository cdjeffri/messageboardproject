using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageBoard.Models;
using MessageBoard.Services;
using System.Web.Security;
using MessageBoard.Services.Users;

namespace MessageBoard.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController()
        {
            var encryptor = new SHA256Encryptor();
            this.userService = new UserService(encryptor);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            bool exists = this.userService.Exists(model.Username);
            bool isAuthenticated = this.userService.Authenticate(model.Username, model.Password);

            if (isAuthenticated == true)
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                return RedirectToAction("Index", "Home");
            }
            else if (exists)
            {
                this.ModelState.AddModelError("", "Username doesn't exist");
                return View(model);
            }
            else
            {
                this.ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            bool exists = this.userService.Exists(user.Username);
            bool usernameInput = this.userService.InputProvided(user.Username);
            bool passwordInput = this.userService.InputProvided(user.Password);
            bool emailInput = this.userService.InputProvided(user.Email);

            if (exists)
            {
                this.ModelState.AddModelError("", "Username already exists");
                return View();
            }

            if (usernameInput == false || passwordInput == false || emailInput == false)
            {
                this.ModelState.AddModelError("", "Missing user input");
                return View();
            }

            try
            {
                this.userService.Register(user);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "An error has occured.");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}