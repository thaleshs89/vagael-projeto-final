using el.api.locathales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using el.api.locathales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace el.api.locathales.Infrastructure.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(cliente => cliente.Id);

            builder.Property(cliente => cliente.Nome)
                .HasMaxLength(250)
                .IsRequired(true);

            builder.OwnsOne(c => c.Cpf, CPF =>
            {
                CPF.Property(c => c.Numero)
                    .IsRequired()
                    .HasColumnName("Cpf")
                    .HasMaxLength(11);
            });

            builder.OwnsOne(c => c.Email, Email =>
            {
                Email.Property(c => c.Endereco)
                    .IsRequired(true)
                    .HasColumnName("Email")
                    .HasMaxLength(250);
            });

            builder.OwnsOne(c => c.Status, Status =>
            {
                Status.Property(c => c.Descricao)
                    .IsRequired()
                    .HasColumnName("DescricaoStatus")
                    .HasMaxLength(50);

                Status.Property(c => c.Value)
                   .IsRequired()
                   .HasColumnName("ValorStatus");
            });

            builder.Property(cliente => cliente.DataCriacao)
                .IsRequired(true);

            builder.Property(cliente => cliente.DataModificacao)
                .IsRequired(true);

            builder.Property(cliente => cliente.DataNascimento)
                .IsRequired(true);
        }
    }
}
