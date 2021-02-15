using AutoMapper;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.ValueObjects;
using el.api.locathales.Infrastructure.DbModels;
using System;

namespace el.api.locathales.Infrastructure.Mapping
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<ClienteDbModel, Cliente>()
                   .ForMember(dest => dest.Nome, m => m.Ignore())
                   .ForMember(dest => dest.Cpf, m => m.Ignore())
                   .ForMember(dest => dest.Email, m => m.Ignore())
                   .ForMember(dest => dest.DataNascimento, m => m.Ignore())
                   .ConstructUsing(src =>
                       new Cliente(
                           src.Nome,
                           new CPF(src.Cpf),
                           new Email(src.Email),
                           src.DataNascimento,
                           src.Senha)
                       )
                   .ForMember(dest => dest.Status, m => m.MapFrom(src => string.IsNullOrWhiteSpace(src.Status) ? null : new Status(Convert.ToInt32(src.Status), src.Status)));
        }
    }
}
