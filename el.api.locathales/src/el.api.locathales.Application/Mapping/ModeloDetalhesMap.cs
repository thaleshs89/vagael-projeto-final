using AutoMapper;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.ValueObjects;

namespace el.api.locathales.Application.Mapping
{
    public class ModeloDetalhesMap : Profile
    {
        public ModeloDetalhesMap()
        {
            CreateMap<ModeloDetalhes, ModeloDetalhesModel>().ReverseMap();
        }
    }
}
