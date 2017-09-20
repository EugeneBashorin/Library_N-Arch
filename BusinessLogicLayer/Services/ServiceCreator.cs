using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class ServiceCreator : IServiceCreatoor
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}