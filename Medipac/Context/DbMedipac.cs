using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Context;

public partial class DbMedipac : DbContext
{
    public DbMedipac()
    {
    }

    public DbMedipac(DbContextOptions<DbMedipac> options)
        : base(options)
    {
    }

    public virtual DbSet<AdmCarruselNoticias> AdmCarruselNoticias { get; set; }

    public virtual DbSet<AdmNoticias> AdmNoticias { get; set; }

    public virtual DbSet<CliHistorialPaciente> CliHistorialPaciente { get; set; }

    public virtual DbSet<CliMedico> CliMedico { get; set; }

    public virtual DbSet<CliPacientes> CliPacientes { get; set; }

    public virtual DbSet<CliRecetaPaciente> CliRecetaPaciente { get; set; }

    public virtual DbSet<ComEstadosUsuario> ComEstadosUsuario { get; set; }

    public virtual DbSet<ComUsuario> ComUsuario { get; set; }

    public virtual DbSet<LogUsuario> LogUsuario { get; set; }

    public virtual DbSet<ResAgenda> ResAgenda { get; set; }

    public virtual DbSet<ResEspecialidades> ResEspecialidades { get; set; }

    public virtual DbSet<ResMedicoEspecialidad> ResMedicoEspecialidad { get; set; }

    public virtual DbSet<ResReserva> ResReserva { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = medipac.database.windows.net; Database = medipac; User Id = mediadmin; Password = Capstone321; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdmCarruselNoticias>(entity =>
        {
            entity.HasOne(d => d.IdNoticiaNavigation).WithMany(p => p.AdmCarruselNoticias)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADM_Carrusel_Noticias_ADM_Noticias");
        });

        modelBuilder.Entity<AdmNoticias>(entity =>
        {
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.AdmNoticias)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADM_Noticias_COM_Usuario");
        });

        modelBuilder.Entity<CliHistorialPaciente>(entity =>
        {
            entity.Property(e => e.Estado).HasComment("Columna que representa el borrado lógico del registro.");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.CliHistorialPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLI_Historial_Paciente_CLI_Medico");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.CliHistorialPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLI_Historial_Paciente_CLI_Pacientes");
        });

        modelBuilder.Entity<CliMedico>(entity =>
        {
            entity.Property(e => e.Dv).IsFixedLength();
            entity.Property(e => e.Estado).HasComment("Columna que representa el borrado lógico del registro.");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.CliMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLI_Medico_COM_Usuario");
        });

        modelBuilder.Entity<CliPacientes>(entity =>
        {
            entity.Property(e => e.Estado).HasComment("Columna que representa el borrado lógico del registro.");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.CliPacientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLI_Pacientes_COM_Usuario");
        });

        modelBuilder.Entity<CliRecetaPaciente>(entity =>
        {
            entity.Property(e => e.Estado).HasComment("Columna que representa el borrado lógico del registro.");

            entity.HasOne(d => d.IdHistorialPacienteNavigation).WithMany(p => p.CliRecetaPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLI_Receta_Paciente_CLI_Historial_Paciente");
        });

        modelBuilder.Entity<ComEstadosUsuario>(entity =>
        {
            entity.Property(e => e.Estado).HasComment("Columna que representa el borrado lógico del registro.");
        });

        modelBuilder.Entity<ComUsuario>(entity =>
        {
            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.ComUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COM_Usuario_COM_Estados_Usuario");
        });

        modelBuilder.Entity<LogUsuario>(entity =>
        {
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LogUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOG_Usuario_COM_Usuario");
        });

        modelBuilder.Entity<ResAgenda>(entity =>
        {
            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.ResAgenda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RES_Agenda_CLI_Medico");
        });

        modelBuilder.Entity<ResEspecialidades>(entity =>
        {
            entity.Property(e => e.Estado).HasComment("Columna que representa el borrado lógico del registro.");
        });

        modelBuilder.Entity<ResMedicoEspecialidad>(entity =>
        {
            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.ResMedicoEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RES_Medico_Especialidad_RES_Especialidades");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.ResMedicoEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RES_Medico_Especialidad_CLI_Medico");
        });

        modelBuilder.Entity<ResReserva>(entity =>
        {
            entity.Property(e => e.Estado).HasComment("Columna que representa el borrado lógico del registro.");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.ResReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RES_Reserva_CLI_Medico");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.ResReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RES_Reserva_CLI_Pacientes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
