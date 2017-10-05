using Entities.Entities;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface INewspaperService
    {
        Newspaper GetNewspaper(int? id);
        List<Newspaper> GetNewspapers();
        void AddNewspaper(Newspaper newspaper);
        void UpdateNewspaper(int? id, Newspaper newspaper);
        void DeleteNewspaper(int? id);
        void GetNewspapersTxtList();
        void GetNewspapersXmlList();
        List<Newspaper> CheckNewspaperPublisher(string publisherName);
        List<string> GetNewspapersPublishers();
    }
}
