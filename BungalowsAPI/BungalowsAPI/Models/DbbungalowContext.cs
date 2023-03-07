using Microsoft.EntityFrameworkCore;

namespace BungalowsAPI.Models;

public partial class DbbungalowContext : DbContext
{
    public DbbungalowContext()
    {
    }

    public DbbungalowContext(DbContextOptions<DbbungalowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<TipoReserva> TipoReservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__0E49C69DE44580DB");

            entity.ToTable("Reserva");

            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaSalida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdTipoReserva)
                .HasConstraintName("FK__Reserva__IdTipoR__38996AB5");
        });

        modelBuilder.Entity<TipoReserva>(entity =>
        {
            entity.HasKey(e => e.IdTipoReserva).HasName("PK__TipoRese__F0D656563251C80B");

            entity.ToTable("TipoReserva");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
