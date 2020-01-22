namespace SistemaCC
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=BDControlCambio")
        {
        }

        public virtual DbSet<Actividades> Actividades { get; set; }
        public virtual DbSet<ActividadesControl> ActividadesControl { get; set; }
        public virtual DbSet<Autorizaciones> Autorizaciones { get; set; }
        public virtual DbSet<ControlCambio> ControlCambio { get; set; }
        public virtual DbSet<ControlServicio> ControlServicio { get; set; }
        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<Monitoreo> Monitoreo { get; set; }
        public virtual DbSet<Notificaciones> Notificaciones { get; set; }
        public virtual DbSet<Revisiones> Revisiones { get; set; }
        public virtual DbSet<Riesgos> Riesgos { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<ServiciosAplicaciones> ServiciosAplicaciones { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividades>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Actividades>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Actividades>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Actividades>()
                .HasMany(e => e.ActividadesControl)
                .WithOptional(e => e.Actividades)
                .HasForeignKey(e => e.fk_Ac);

            modelBuilder.Entity<Autorizaciones>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<ControlCambio>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<ControlCambio>()
                .Property(e => e.Introduccion)
                .IsUnicode(false);

            modelBuilder.Entity<ControlCambio>()
                .Property(e => e.Objetivos)
                .IsUnicode(false);

            modelBuilder.Entity<ControlCambio>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<ControlCambio>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<ControlCambio>()
                .HasMany(e => e.ActividadesControl)
                .WithOptional(e => e.ControlCambio)
                .HasForeignKey(e => e.fk_CC);

            modelBuilder.Entity<ControlCambio>()
                .HasMany(e => e.Autorizaciones)
                .WithOptional(e => e.ControlCambio)
                .HasForeignKey(e => e.fk_CC);

            modelBuilder.Entity<ControlCambio>()
                .HasMany(e => e.ControlServicio)
                .WithOptional(e => e.ControlCambio)
                .HasForeignKey(e => e.fk_CC);

            modelBuilder.Entity<ControlCambio>()
                .HasMany(e => e.Documentos)
                .WithOptional(e => e.ControlCambio)
                .HasForeignKey(e => e.fk_CC);

            modelBuilder.Entity<ControlCambio>()
                .HasMany(e => e.Notificaciones)
                .WithOptional(e => e.ControlCambio)
                .HasForeignKey(e => e.fk_CC);

            modelBuilder.Entity<ControlCambio>()
                .HasMany(e => e.Revisiones)
                .WithOptional(e => e.ControlCambio)
                .HasForeignKey(e => e.fk_CC);

            modelBuilder.Entity<ControlCambio>()
                .HasMany(e => e.Riesgos)
                .WithOptional(e => e.ControlCambio)
                .HasForeignKey(e => e.fk_CC);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.DocPath)
                .IsUnicode(false);

            modelBuilder.Entity<Monitoreo>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Notificaciones>()
                .Property(e => e.Contenido)
                .IsUnicode(false);

            modelBuilder.Entity<Revisiones>()
                .Property(e => e.InfGeneral)
                .IsUnicode(false);

            modelBuilder.Entity<Revisiones>()
                .Property(e => e.Actividades)
                .IsUnicode(false);

            modelBuilder.Entity<Revisiones>()
                .Property(e => e.Riesgos)
                .IsUnicode(false);

            modelBuilder.Entity<Revisiones>()
                .Property(e => e.Servicios)
                .IsUnicode(false);

            modelBuilder.Entity<Riesgos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Riesgos>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.Rol)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.UsuarioRol)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.fk_Rol);

            modelBuilder.Entity<ServiciosAplicaciones>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ServiciosAplicaciones>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ServiciosAplicaciones>()
                .Property(e => e.Acronimo)
                .IsUnicode(false);

            modelBuilder.Entity<ServiciosAplicaciones>()
                .HasMany(e => e.ControlServicio)
                .WithOptional(e => e.ServiciosAplicaciones)
                .HasForeignKey(e => e.fk_SA);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.ApePaterno)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.ApeMaterno)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NoExt)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.ClaveUnica)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Actividades)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.Responsable);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Autorizaciones)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.fk_U);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.ControlCambio)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.Creador);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Monitoreo)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.fk_U);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Notificaciones)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.fk_U);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.ServiciosAplicaciones)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.Dueno);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.UsuarioRol)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.fk_Us);
        }
    }
}
