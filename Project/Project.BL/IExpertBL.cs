using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Project.BL {
    public interface IExpertBL {
        ExpertDTO InsertExpert (ExpertDTO expert);
        List<ExpertDTO> GetExperts ();
        ExpertDTO GetExpertById (int id);
        List<ExpertDTO> GetFilteredExperts (int category, int subject,
            int city, string name);
        List<ExpertDTO> GetAllExperts ();
        List<ExpertDTO> GetAllActiveExperts ();

        ExpertDTO ChangeStatus (ExpertDTO expert);
        //List<ExpertDTO> GetExpertsByPage(int pageIndex, int pageNumber);
        bool IsExpert (int id);
        ExpertDTO PutExpert (ExpertDTO expert);
        bool uploadImg (int id, string filename);

    }
}