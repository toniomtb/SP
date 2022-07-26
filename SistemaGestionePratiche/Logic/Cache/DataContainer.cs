using System.Collections.Generic;
using SP.Model.Models;

namespace SP.Logic.Cache
{
    public class DataContainer : IDataContainer
    {
        List<Pratica> _listaPratiche;

        public DataContainer()
        {
            _listaPratiche = new List<Pratica>();
        }

        public List<Pratica> GetAllPratiche()
        {
            return _listaPratiche;
        }

        public void AddPratica(Pratica pratica)
        {
            _listaPratiche.Add(pratica);
        }
    }
}
