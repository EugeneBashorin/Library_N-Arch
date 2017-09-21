using DataAccessLayer.EntityFrameworkContext;
using DataAccessLayer.Interfaces;
using Entities.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccessLayer.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

//************************************************************************GET_USER_LIST*****************************************************************************       
        public List<ClientProfile> GetUsersList()
        {
            List<ClientProfile> clientList = new List<ClientProfile>();
            var users = Database.ClientProfiles;
            foreach (var clients in users)
            {
                clientList.Add(clients);
            }
            return clientList;
        }

//*************************************************************************Update_USER_BannedState****************************************************************************
        public void UpdateBannState(string userId, bool bannedState)
        {
            ClientProfile user = Database.ClientProfiles.Find(userId);
            if(user != null)
            user.IsBanned = bannedState;
            Database.Entry(user).State = EntityState.Modified;
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}