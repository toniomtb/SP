using System.ComponentModel.DataAnnotations;

namespace SP.Model.RequestModels
{
    public class RequestPratica
    {
        [Required]
        public RequestUtente Utente { get; set; }
        [Required]
        public string AllegatoPratica { get; set; }
    }
}
