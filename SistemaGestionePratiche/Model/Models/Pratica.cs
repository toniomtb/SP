using System.Collections;

namespace SP.Model.Models
{
    public class Pratica
    {
        public int IdPratica { get; set; }
        public Utente Utente { get; set; }
        public string AllegatoPratica { get; set; }
        public StatoPratica StatoPratica { get; set; }
        public bool? Approvata { get; set; }
        public Queue CronologiaStati { get; set; }
    }
}
