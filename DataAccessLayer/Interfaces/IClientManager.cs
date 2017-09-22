using Entities.Entities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
        List<ClientProfile> GetUsersList();
        void UpdateBannState(string userId, string IsBanned);
    }
}
