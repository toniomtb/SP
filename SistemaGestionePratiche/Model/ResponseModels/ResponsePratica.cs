using System.Collections;
using SP.Model.Models;

namespace SP.Model.ResponseModels
{
    public class ResponsePratica
    {
        public int IdPratica { get; set; }
        public ResponseUtente Utente { get; set; } 
        public StatoPratica StatoPratica { get; set; }
        public bool? Approvata { get; set; }
        public Queue CronologiaStati { get; set; }
    }
}
