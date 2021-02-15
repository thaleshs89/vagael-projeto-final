using AutoMapper;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.ValueObjects;
using System;

namespace el.api.locathales.Application.Mapping
{
    public class OperadorMap : Profile
    {
        public OperadorMap()
        {
            CreateMap<Operador, OperadorModel>()
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula.Numero))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Email, m => m.MapFrom(src => src.Email.ToString()));


            CreateMap<OperadorModel, Operador>()
                .ForMember(dest => dest.Nome, m => m.Ignore())
                .ForMember(dest => dest.Matricula, m => m.Ignore())
                .ForMember(dest => dest.Email, m => m.Ignore())
                .ForMember(dest => dest.Id, m => m.Ignore())
                .ConstructUsing(src =>
                    new Operador(
                        src.Nome,
                        new Matricula(src.Matricula),
                        new Email(src.Email))
                    )
                .ForMember(dest => dest.Status, m => m.MapFrom(src => string.IsNullOrWhiteSpace(src.Status) ? null : new Status(Convert.ToInt32(src.Status), src.Status)));
        }
    }
}
