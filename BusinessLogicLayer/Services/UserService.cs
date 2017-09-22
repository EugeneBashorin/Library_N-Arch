using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Entities.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        IUnitOfWorkIdentity Database { get; set; }

        public UserService(IUnitOfWorkIdentity unitOfWork)
        {
            Database = unitOfWork;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email, IsBanned = "false" };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Name = userDto.Name, IsBanned = "false" };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);           
            if (user != null)
            {              
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);               
            }
            if (user.IsBanned == "true")
            {
                //**********Заглушка возврата***********//
                claim = null;
                //**********Заглушка возврата***********//
            }
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        //********************************************GET_USERS****************************
        public List<UserDTO> GetUsers()
        {
            List<UserDTO> userList = new List<UserDTO>();
            var clientProfileList = Database.ClientManager.GetUsersList();
            foreach (var user in clientProfileList)
            {
                userList.Add(new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.ApplicationUser.Email,
                    IsBanned = user.IsBanned,
                });
            }
            return userList;
        }

        //*******************************GET_USERS*****************************
        public void UpdateBannState(string Id, string bannedState)
        {
            Database.ClientManager.UpdateBannState(Id, bannedState);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}