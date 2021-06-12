using Microsoft.EntityFrameworkCore;
using ProAgil.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Data
{
    public class ProAgilDbContext : DbContext
    {
        public DbSet<Evento> EVENTO { get; set; }

        public ProAgilDbContext(DbContextOptions<ProAgilDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>(meal =>
            {
                meal.HasKey(property => property.EventoId);
            });
        }
    }
}
