using System;
using Entidades.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Datos
{
    public partial class SegurosContext : DbContext
    {

        public SegurosContext(DbContextOptions<SegurosContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<ItemMenu> ItemMenu { get; set; }
        public virtual DbSet<LogException> LogException { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Poliza> Poliza { get; set; }
        public virtual DbSet<Seguro> Seguro { get; set; }
        public virtual DbSet<Siniestro> Siniestro { get; set; }
        public virtual DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=WINDOWS-Q41LRQT\SQLEXPRESS;Database=Seguros;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.IdColor);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemMenu>(entity =>
            {
                entity.HasKey(e => e.IdSubMenu);

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.ItemMenu)
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMenu_Menu");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.ItemMenu)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMenu_Perfil");
            });

            modelBuilder.Entity<LogException>(entity =>
            {
                entity.HasKey(e => e.IdLog);

                entity.Property(e => e.FechaLog).HasColumnType("datetime");

                entity.Property(e => e.Mensaje).HasColumnType("text");

                entity.Property(e => e.Metodo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Trace).HasColumnType("text");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.IdLogin);

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCambio).HasColumnType("datetime");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Login_Perfil");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Login_Persona");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu);

                entity.Property(e => e.TipoMenu).HasColumnType("char(1)");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Url).HasColumnType("text");
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.HasKey(e => e.IdModelo);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdGenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Genero");
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.HasKey(e => e.IdPoliza);

                entity.Property(e => e.Factura)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCoverturaF).HasColumnType("datetime");

                entity.Property(e => e.FechaCoverturaI)
                    .HasColumnName("Fecha_CoverturaI")
                    .HasColumnType("datetime");

                entity.Property(e => e.NumPoliza)
                    .HasColumnName("numPoliza")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TotValAsegurado)
                    .HasColumnName("Tot_Val_Asegurado")
                    .HasColumnType("numeric(15, 2)");

                entity.Property(e => e.TotValPrima)
                    .HasColumnName("Tot_Val_Prima")
                    .HasColumnType("numeric(15, 2)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Poliza)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Poliza_Persona");
            });

            modelBuilder.Entity<Seguro>(entity =>
            {
                entity.HasKey(e => e.IdSeguro);

                entity.Property(e => e.PrimaSeguro)
                    .HasColumnName("Prima_Seguro")
                    .HasColumnType("numeric(15, 2)");

                entity.Property(e => e.Tasa).HasColumnType("numeric(15, 2)");

                entity.Property(e => e.ValAsegurado)
                    .HasColumnName("Val_Asegurado")
                    .HasColumnType("numeric(15, 2)");

                entity.HasOne(d => d.IdPolizaNavigation)
                    .WithMany(p => p.Seguro)
                    .HasForeignKey(d => d.IdPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Seguro_Poliza");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Seguro)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Seguro_Vehiculo");
            });

            modelBuilder.Entity<Siniestro>(entity =>
            {
                entity.HasKey(e => e.IdSiniestro);

                entity.Property(e => e.CallePrincipal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CalleSecundaria)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Referencia).HasColumnType("text");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Siniestro)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Siniestro_Vehiculo");
            });

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.IdTipoVehiculo);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo);

                entity.Property(e => e.AnioDeFabricacion)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Chasis)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.Placa)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdColorNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdColor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Color");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Marca");

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdModelo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Modelo");

                entity.HasOne(d => d.IdTipoVehiculoNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdTipoVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_TipoVehiculo");
            });
        }
    }
}
