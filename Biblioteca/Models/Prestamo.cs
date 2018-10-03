namespace Biblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prestamo")]
    public partial class Prestamo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prestamo()
        {
            detallePrestamo = new HashSet<detallePrestamo>();
            Devoluciones = new HashSet<Devoluciones>();
        }

        [Key]
        public int Id_prestamo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El formato de fecha es incorrecto")]
        public DateTime Fecha_entrega { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El formato de fecha es incorrecto")]
        public DateTime? Fecha_devolucion { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        public int Id_usuario { get; set; }

        public int Id_cliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallePrestamo> detallePrestamo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Devoluciones> Devoluciones { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
