namespace Biblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Libro")]
    public partial class Libro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Libro()
        {
            detallePrestamo = new HashSet<detallePrestamo>();
        }

        [Key]
        public int Id_libro { get; set; }
        [Required(ErrorMessage = "Debe agregar el ISBN")]
        
        public int ISBN { get; set; }

        [Required(ErrorMessage = "El titulo del libro es requerido")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Debe agregar la edicion del libro")]
        [StringLength(50)]
        public string Edicion { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El formato de fecha es incorrecto")]
        public DateTime Fecha_lanzamiento { get; set; }

        [Required(ErrorMessage = "Debe de agregar una descripción")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe de agregar un idioma")]
        [StringLength(50)]
        public string Idioma { get; set; }

        public int NumeroPaginas { get; set; }

        public int Cantidad { get; set; }

        public int Id_categoria { get; set; }

        public int Id_autor { get; set; }

        public int Id_editorial { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual Categoria Categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallePrestamo> detallePrestamo { get; set; }

        public virtual Editorial Editorial { get; set; }
    }
}
