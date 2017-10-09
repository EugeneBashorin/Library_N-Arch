using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using ConfigurationData.Configurations;
using LibraryProject.ViewModels;
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
                    ModelState.AddModelError("", "Wrong Login or Password or your profile was banned.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Books", "Home");
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Books", "Home");
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

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Role = "user",
                    IsBanned = "false"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Books", "Home");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO { Email = IdentityConfiguration._ADMIN_EMAIL, UserName = IdentityConfiguration._ADMIN_EMAIL, Password = IdentityConfiguration._ADMIN_PASSWORD, Name = IdentityConfiguration._ADMIN_NAME, Role = IdentityConfiguration._ADMIN_ROLE, IsBanned = "false" },
                                             new List<string> { IdentityConfiguration._USER_ROLE, IdentityConfiguration._ADMIN_ROLE });
        }

        [HttpGet]
        [Authorize(Roles = IdentityConfiguration._ADMIN_ROLE)]
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
    }
}