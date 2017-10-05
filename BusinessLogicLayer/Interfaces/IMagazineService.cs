using Entities.Entities;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMagazineService
    {
        Magazine GetMagazine(int? id);
        List<Magazine> GetMagazines();
        void AddMagazine(Magazine magazine);
        void UpdateMagazine(int? id, Magazine magazine);
        void DeleteMagazine(int? id);
        void GetMagazinesTxtList();
        void GetMagazinesXmlList();
        List<Magazine> CheckMagazinePublisher(string publisherName);
        List<string> GetMagazinesPublishers();
    }
}