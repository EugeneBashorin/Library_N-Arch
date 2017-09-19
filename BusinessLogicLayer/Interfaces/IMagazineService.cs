using BusinessLogicLayer.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMagazineService
    {
        MagazineDTO GetMagazine(int? id);
        List<MagazineDTO> GetMagazines();
        void AddItem(MagazineDTO magazineDto);
        void Update(int id, MagazineDTO magazineDTO);
        void Delete(int id);
        void GetTxtList();
        void GetXmlList();
    }
}