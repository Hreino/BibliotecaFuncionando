namespace Biblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("detallePrestamo")]
    public partial class detallePrestamo
    {
        public int id { get; set; }

        public int idLibro { get; set; }

        public int idPrestamo { get; set; }
        [Required(ErrorMessage = "Debe agregar una cantidad")]
        public int cantidad { get; set; }

        public virtual Libro Libro { get; set; }

        public virtual Prestamo Prestamo { get; set; }
    }
}
