using el.api.locathales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using el.api.locathales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace el.api.locathales.Infrastructure.Configuration
{
    public class ContratoLocacaoConfiguration : IEntityTypeConfiguration<ContratoLocacao>
    {
        public void Configure(EntityTypeBuilder<ContratoLocacao> builder)
        {
            builder.ToTable("Contrato");

            builder.HasKey(contrato => contrato.Id);
        }
    }
}
