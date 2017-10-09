using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using LibraryProject.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(LibraryProject.App_Start.Startup))]
namespace LibraryProject.App_Start
{
    public class Startup
    {
        ServiceCreator serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("IdentityDataBase");
        }
    }
}
