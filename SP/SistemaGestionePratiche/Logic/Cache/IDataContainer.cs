using System.Collections.Generic;
using SP.Model.Models;

namespace SP.Logic.Cache
{
    public interface IDataContainer
    {
        List<Pratica> GetAllPratiche();
        void AddPratica(Pratica pratica);
    }
}
