using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Entities.Entities;
using Ninject.Modules;

namespace BusinessLogicLayer.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
           Bind<IBookRepository>().To<BookRepository>().WithConstructorArgument(connectionString);
           Bind<IMagazineRepository>().To<MagazineRepository>().WithConstructorArgument(connectionString);
           Bind<INewspaperRepository>().To<NewspaperRepository>().WithConstructorArgument(connectionString);
        }
    }
}