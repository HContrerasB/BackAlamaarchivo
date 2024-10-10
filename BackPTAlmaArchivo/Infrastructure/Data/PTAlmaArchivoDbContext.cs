using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackPTAlmaArchivo.Infrastructure.Data;

public partial class PTAlmaArchivoDbContext : DbContext
{
    public PTAlmaArchivoDbContext()
    {
    }

    public PTAlmaArchivoDbContext(DbContextOptions<PTAlmaArchivoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<PersonasUsuario> PersonasUsuarios { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KEITOR\\MSSQLSERVER01;Database=PTAlmaArchivoDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personas__3214EC07AFEC1397");

            entity.HasIndex(e => e.Email, "UQ__Personas__A9D105346C7BF5A1").IsUnique();

            entity.HasIndex(e => e.NumeroIdentificacion, "UQ__Personas__FCA68D9126EF6489").IsUnique();

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PersonasUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personas__3214EC07D64498F6");

            entity.HasIndex(e => new { e.PersonaId, e.UsuarioId }, "UQ_PersonaUsuario").IsUnique();

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Persona).WithMany(p => p.PersonasUsuarios)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona");

            entity.HasOne(d => d.Usuario).WithMany(p => p.PersonasUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC0787B978D2");

            entity.HasIndex(e => e.Usuario, "UQ__Usuarios__E3237CF7E048553F").IsUnique();

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Pass)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
