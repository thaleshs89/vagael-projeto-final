using el.api.locathales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using el.api.locathales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace el.api.locathales.Infrastructure.Configuration
{
    public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo");

            builder.HasKey(veiculo => veiculo.Id);
        }
    }
}
