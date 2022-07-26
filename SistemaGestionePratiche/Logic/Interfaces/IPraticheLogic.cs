using SP.Model.Models;

namespace SP.Logic.Interfaces
{
    public interface IPraticheLogic
    {
        int CreateNewPratica(Pratica nuovaPratica);
        void UpdatePratica(int idPratica, Pratica updatedPratica);
        void ProgressPratica(int idPratica);
        Pratica GetPratica(int idPratica);
        string GetPdf(int idPratica);
    }
}
