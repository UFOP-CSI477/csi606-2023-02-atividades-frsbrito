using atividade_pratica.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace atividade_pratica.Context
{
    public class AtividadeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AtividadeDbContext(DbContextOptions<AtividadeDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Doacao> Doacao { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<LocalColeta> LocalColeta { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<TipoSanguineo> TipoSanguineo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doacao>()
                .HasOne(d => d.Pessoa)
                .WithMany()
                .HasForeignKey(d => d.PessoaId)
                .OnDelete(DeleteBehavior.Restrict); // Evita deleção em cascata

            modelBuilder.Entity<Doacao>()
                .HasOne(d => d.LocalColeta)
                .WithMany()
                .HasForeignKey(d => d.LocalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}