using el.api.locathales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using el.api.locathales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace el.api.locathales.Infrastructure.Configuration
{
    public class OperadorConfiguration : IEntityTypeConfiguration<Operador>
    {
        public void Configure(EntityTypeBuilder<Operador> builder)
        {
            builder.ToTable("Operador");

            builder.HasKey(operador => operador.Id);

            builder.Property(operador => operador.Nome)
                .HasMaxLength(250)
                .IsRequired(true);

            builder.OwnsOne(operador => operador.Matricula, Matricula =>
            {
                Matricula.Property(matricula => matricula.Numero)
                    .IsRequired(true)
                    .HasColumnName("Numero")
                    .HasMaxLength(250);
            });


            builder.OwnsOne(operador => operador.Email, Email =>
            {
                Email.Property(operador => operador.Endereco)
                    .IsRequired(true)
                    .HasColumnName("Email")
                    .HasMaxLength(250);
            });

            builder.OwnsOne(operador => operador.Status, Status =>
            {
                Status.Property(operador => operador.Descricao)
                    .IsRequired()
                    .HasColumnName("DescricaoStatus")
                    .HasMaxLength(50);

                Status.Property(operador => operador.Value)
                   .IsRequired()
                   .HasColumnName("ValorStatus");
            });

            builder.Property(operador => operador.DataCriacao)
                .IsRequired(true);

            builder.Property(operador => operador.DataModificacao)
                .IsRequired(true);
        }
    }
}
