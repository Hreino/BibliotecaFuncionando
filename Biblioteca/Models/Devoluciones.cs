namespace Biblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Devoluciones
    {
        [Key]
        public int Id_devoluciones { get; set; }

        public int Id_prestamo { get; set; }


        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es valido")]
        [Column(TypeName = "date")]
        public DateTime FechaDevolucion { get; set; }

        [Required(ErrorMessage = "Debe agregar una observación")]
        [StringLength(75)]
        public string Observaciones { get; set; }

        public virtual Prestamo Prestamo { get; set; }
    }
}
