using AutoMapper;
using SP.Model.Models;
using SP.Model.RequestModels;
using SP.Model.ResponseModels;

namespace SP.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //REQUEST
            CreateMap<RequestPratica, Pratica>()
                .ForMember(x => x.StatoPratica, opt => opt.Ignore())
                .ForMember(x => x.CronologiaStati, opt => opt.Ignore())
                .ForMember(x => x.IdPratica, opt => opt.Ignore())
                .ForMember(x => x.Approvata, opt => opt.Ignore());
            CreateMap<RequestUtente, Utente>();


            //RESPONSE
            CreateMap<Pratica, ResponsePratica>();
            CreateMap<Utente, ResponseUtente>();
        }
    }
}
