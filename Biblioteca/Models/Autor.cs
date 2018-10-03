namespace Biblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Autor")]
    public partial class Autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor()
        {
            Libro = new HashSet<Libro>();
        }

        [Key]

        public int Id_autor { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]

        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Escriba el sexo")]
        [StringLength(10)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "El pais es requerido")]
        [StringLength(50)]
        public string Pais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Libro> Libro { get; set; }
    }
}
