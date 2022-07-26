using System;

namespace SP.Model.ResponseModels
{
    public class ResponseUtente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }
        public DateTime DataDiNascita { get; set; }
    }
}
