using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaEventos.Domain.Entity;

#nullable disable

namespace SistemaEventos.DAL
{
    public partial class SistemaEventosContext : DbContext
    {

        public SistemaEventosContext(DbContextOptions<SistemaEventosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaEvento> CategoriaEventos { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Participacao> Participacaos { get; set; }
        public virtual DbSet<StatusEvento> StatusEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<CategoriaEvento>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaEvento);

                entity.ToTable("CategoriaEvento");

                entity.Property(e => e.NomeCategoria)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento);

                entity.ToTable("Evento");

                entity.Property(e => e.DataHoraFim).HasColumnType("datetime");

                entity.Property(e => e.DataHoraInicio).HasColumnType("datetime");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Local)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaEventoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdCategoriaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento_CategoriaEvento");

                entity.HasOne(d => d.IdEventoStatusNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdEventoStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento_EventoStatus");
            });

            modelBuilder.Entity<Participacao>(entity =>
            {
                entity.HasKey(e => e.IdParticipacao);

                entity.ToTable("Participacao");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LoginParticipante)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Participacaos)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participacao_Evento");
            });

            modelBuilder.Entity<StatusEvento>(entity =>
            {
                entity.HasKey(e => e.IdEventoStatus);

                entity.ToTable("StatusEvento");

                entity.Property(e => e.NomeStatus)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
