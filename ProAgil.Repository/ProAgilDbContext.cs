using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.Repository
{
    public class ProAgilDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(property => new { property.UserId, property.RoleId });
                userRole.HasOne(property => property.Role).WithMany(property => property.UserRoles).HasForeignKey(property => property.RoleId).IsRequired();
                userRole.HasOne(property => property.User).WithMany(property => property.UserRoles).HasForeignKey(property => property.UserId).IsRequired();
            });

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