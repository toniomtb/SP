using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Model.RequestModels
{
    public class RequestUtente
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        public string CodiceFiscale { get; set; }
        [Required]
        public DateTime DataDiNascita { get; set; }
    }
}
