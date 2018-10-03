namespace Biblioteca.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<detallePrestamo> detallePrestamo { get; set; }
        public virtual DbSet<Devoluciones> Devoluciones { get; set; }
        public virtual DbSet<Editorial> Editorial { get; set; }
        public virtual DbSet<Libro> Libro { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Sexo)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Libro)
                .WithRequired(e => e.Autor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.Categoria1)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Libro)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Prestamo)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devoluciones>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Editorial>()
                .HasMany(e => e.Libro)
                .WithRequired(e => e.Editorial)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Edicion)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Idioma)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .HasMany(e => e.detallePrestamo)
                .WithRequired(e => e.Libro)
                .HasForeignKey(e => e.idLibro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Prestamo>()
                .HasMany(e => e.detallePrestamo)
                .WithRequired(e => e.Prestamo)
                .HasForeignKey(e => e.idPrestamo);

            modelBuilder.Entity<Prestamo>()
                .HasMany(e => e.Devoluciones)
                .WithRequired(e => e.Prestamo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.Rol)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Prestamo)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);
        }
    }
}
