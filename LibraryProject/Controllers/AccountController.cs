using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using Entities.Entities;
using LibraryProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }                 
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();

            //if (ModelState.IsValid)
            //{
            //    var user = new ApplicationUser {  UserName = model.Email, Email = model.Email,  IsBanned = "false" };
            //    var result = await UserService.Create(user);
            //    //if (result.Succeeded)
            //}

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Role = "user",
                    IsBanned = "false"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)                    
                return RedirectToAction("Index", "Home");
                //return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Name = "Semen",
                Role = "admin",
                IsBanned = "false"
            }, 
            new List<string> { "user", "admin" }
            );
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            List<UserDTO> usersList = UserService.GetUsers();
            List<ManageUsersModel> users = new List<ManageUsersModel>();
            foreach (var a in usersList)
            {
                users.Add(new ManageUsersModel { Id = a.Id, Email = a.Email, Name = a.Name, IsBanned = a.IsBanned });
            }            
            return View(users);
        }

        [HttpPost]
        public ActionResult SetBannUser(string id, string banned)
        {
            UserService.UpdateBannState(id, banned);
            return RedirectToAction("Index");
        }

        //private ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //}

        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = new ApplicationUser();
        //        user.UserName = model.Email;
        //        user.Email = model.Email;

        //        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await UserManager.AddToRoleAsync(user.Id, "user");
        //            return RedirectToAction("Login", "Account");
        //        }
        //        else
        //        {
        //            foreach (string error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error);
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.logoutLinkElement = ViewsElementsConfiguration._ATTRIBUTES_STATE_OFF;
        //    ViewBag.returnUrl = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("", "Wrong login or password.");
        //        }
        //        else
        //        {
        //            ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //            AuthenticationManager.SignOut();
        //            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, claim);
        //            if (String.IsNullOrEmpty(returnUrl))
        //            {
        //                return RedirectToAction("Index", "Home");
        //            } 
        //            return Redirect(returnUrl);
        //        }
        //    }
        //    ViewBag.returnUrl = returnUrl;
        //    return View(model);
        //}
        //public ActionResult Logout()
        //{
        //    AuthenticationManager.SignOut();
        //    return RedirectToAction("Login","Account");
        //}


        //[HttpGet]
        //public ActionResult Delete()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public async Task<ActionResult> DeleteConfirmed()
        //{
        //    ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //    if (user == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    IdentityResult result = await UserManager.DeleteAsync(user);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
    }
}