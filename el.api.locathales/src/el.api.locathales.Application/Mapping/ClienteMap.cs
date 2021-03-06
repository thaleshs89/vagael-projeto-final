﻿using AutoMapper;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.ValueObjects;
using System;

namespace el.api.locathales.Application.Mapping
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<Cliente, ClienteModel>()
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf.ToString()))
                .ForMember(dest => dest.Email, m => m.MapFrom(src => src.Email.ToString()))
                .ForMember(dest => dest.Status, m => m.MapFrom(src => src.Status.ToString() ));


            CreateMap<ClienteModel, Cliente>()
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
