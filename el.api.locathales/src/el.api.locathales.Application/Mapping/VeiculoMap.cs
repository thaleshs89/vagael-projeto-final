using AutoMapper;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.ValueObjects;

namespace el.api.locathales.Application.Mapping
{
    public class VeiculoMap : Profile
    {
        public VeiculoMap()
        {
            CreateMap<VeiculoModel, Veiculo>()
                .ForMember(dest => dest.Placa, m => m.Ignore())
                .ForMember(dest => dest.Ano, m => m.Ignore())
                .ForMember(dest => dest.Categoria, m => m.Ignore())
                .ForMember(dest => dest.CodigoModelo, m => m.Ignore())
                .ForMember(dest => dest.CodigoMarca, m => m.Ignore())
                .ForMember(dest => dest.Combustivel, m => m.Ignore())
                .ForMember(dest => dest.ValorDia, m => m.Ignore())
                .ForMember(dest => dest.NomeModelo, m => m.Ignore())
                .ForMember(dest => dest.Id, m => m.Ignore())
                .ConstructUsing(src =>
                    new Veiculo(
                        src.Placa,
                        src.CodigoMarca,
                        src.CodigoModelo,
                        src.Ano,
                        src.ValorHora,
                        src.Combustivel,
                        src.LimitePortaMalas,
                        src.Categoria,
                        src.NomeModelo)
                    );
            CreateMap<Veiculo, VeiculoModel>();
        }
    }
}
