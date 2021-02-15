using AutoMapper;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.ValueObjects;

namespace el.api.locathales.Application.Mapping
{
    public class ContratoLocacaoMap : Profile
    {
        public ContratoLocacaoMap()
        {
            CreateMap<ContratoLocacaoModel, ContratoLocacao>()
                .ForMember(dest => dest.Placa, m => m.Ignore())
                .ForMember(dest => dest.CpfCliente, m => m.Ignore())
                .ForMember(dest => dest.DataInicial, m => m.Ignore())
                .ForMember(dest => dest.DataFinal, m => m.Ignore())
                .ForMember(dest => dest.Id, m => m.Ignore())
                .ConstructUsing(src =>
                    new ContratoLocacao(
                        src.Placa,
                        src.DataInicial,
                        src.DataFinal,
                        src.CpfCliente
                        )
                    );
            CreateMap<Veiculo, VeiculoModel>();
        }
    }
}
