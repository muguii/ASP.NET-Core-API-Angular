using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilDbContext : DbContext
    {
        public DbSet<Evento> EVENTO { get; set; }
        public DbSet<Palestrante> PALESTRANTE { get; set; }
        public DbSet<PalestranteEvento> PALESTRANTE_EVENTO { get; set; }
        public DbSet<Lote> LOTE { get; set; }
        public DbSet<RedeSocial> REDE_SOCIAL { get; set; }

        public ProAgilDbContext(DbContextOptions<ProAgilDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>(evento =>
            {
                evento.HasKey(property => property.Id);
            });

            modelBuilder.Entity<Palestrante>(palestrante =>
            {
                palestrante.HasKey(property => property.Id);
            });

            modelBuilder.Entity<PalestranteEvento>().HasKey(palestranteEvento => new { 
                palestranteEvento.EventoId, palestranteEvento.PalestranteId
            });

            modelBuilder.Entity<Lote>(lote =>
            {
                lote.HasKey(property => property.Id);
            });

            modelBuilder.Entity<RedeSocial>(redeSocial =>
            {
                redeSocial.HasKey(property => property.Id);
            });

        }
    }
}