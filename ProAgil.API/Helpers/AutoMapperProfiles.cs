using AutoMapper;
using ProAgil.API.DTOs;
using ProAgil.Domain;
using System.Linq;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfiles : Profile // Essa classe é referenciada por meio de Reflection na DLL do projeto WebAPI (Startup -> AddAutoMapper(typeof(Startup)))
    {
        public AutoMapperProfiles()
        {
            //Adicionando o ReverseMap() é feito o mapeamento bidirecional.
            //       sourceMember / destinationMember 
            CreateMap<Palestrante, PalestranteDTO>().ForMember(destinationMember => destinationMember.Eventos, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.PalestrantesEventos.Select(palestranteEvento => palestranteEvento.Evento))).ReverseMap();
            CreateMap<Evento, EventoDTO>().ForMember(destinationMember => destinationMember.Palestrantes, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.PalestrantesEventos.Select(palestranteEvento => palestranteEvento.Palestrante))).ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
        }
    }
}
