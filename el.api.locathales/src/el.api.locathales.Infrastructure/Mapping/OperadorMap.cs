using AutoMapper;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.ValueObjects;
using el.api.locathales.Infrastructure.DbModels;

namespace el.api.locathales.Infrastructure.Mapping
{
    public class OperadorMap : Profile
    {
        public OperadorMap()
        {
            CreateMap<OperadorDbModel, Operador>()
                .ForMember(dest => dest.Nome, m => m.Ignore())
                .ForMember(dest => dest.Matricula, m => m.Ignore())
                .ForMember(dest => dest.Email, m => m.Ignore())
                .ConstructUsing(src =>
                    new Operador(
                        src.Nome,
                        new Matricula(src.Matricula),
                        new Email(src.Email))
                    );
        }
    }
}
