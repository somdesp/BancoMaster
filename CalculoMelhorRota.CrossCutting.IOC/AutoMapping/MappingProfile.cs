using AutoMapper;
using CalculoMelhorRota.Application.ViewsModels;
using CalculoMelhorRota.Domain.Entity;

namespace EConstrumarket.Construmanager.Core.CrossCutting.IoC.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ResultadoViewModel, Resultado>().ReverseMap();
            CreateMap<RotasViewModel, Rotas>().ReverseMap();  
        }
    }
}
