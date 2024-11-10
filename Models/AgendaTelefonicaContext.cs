using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AgendaTelefonica.Models;

public partial class AgendaTelefonicaContext : DbContext
{
    public AgendaTelefonicaContext()
    {
    }

    public AgendaTelefonicaContext(DbContextOptions<AgendaTelefonicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contato> Contatos { get; set; }

    public virtual DbSet<Telefone> Telefones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Contato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contato");

            entity.HasIndex(e => e.Nome, "idx_nome_contato");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idade).HasColumnName("IDADE");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("NOME");
        });

        modelBuilder.Entity<Telefone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("telefone");

            entity.HasIndex(e => e.IdContato, "IDCONTATO");

            entity.HasIndex(e => e.Numero, "idx_numero_telefone");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdContato).HasColumnName("IDCONTATO");
            entity.Property(e => e.Numero)
                .HasMaxLength(16)
                .HasColumnName("NUMERO");

            entity.HasOne(d => d.IdContatoNavigation)
                .WithMany(p => p.Telefones)
                .HasForeignKey(d => d.IdContato)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("telefone_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
