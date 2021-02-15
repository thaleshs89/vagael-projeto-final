using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace el.api.locathales.Infrastructure.Repositories
{
    public sealed class LocaThalesContext : DbContext, IUnitOfWork
    {
        public LocaThalesContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ContratoLocacao> Contratos { get; set; }
        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            modelBuilder.Ignore<Notification>();


            modelBuilder.Entity<Operador>()
                .OwnsOne(p => p.Status);

            modelBuilder.Entity<Operador>()
                .OwnsOne(p => p.Matricula);

            modelBuilder.Entity<Operador>()
                .OwnsOne(p => p.Email);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=data.db");
        }

        public async Task<bool> PersistirNoBanco()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
