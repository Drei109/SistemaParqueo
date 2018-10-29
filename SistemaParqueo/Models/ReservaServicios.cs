namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReservaServicios
    {
        public int ReservaServiciosId { get; set; }

        public int ReservaId { get; set; }

        public int ServicioId { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        public int? Cantidad { get; set; }

        public virtual Reserva Reserva { get; set; }

        public virtual Servicio Servicio { get; set; }
    }
}
