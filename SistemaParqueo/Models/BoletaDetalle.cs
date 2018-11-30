using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaParqueo.Models
{
    public class BoletaDetalle
    {
        [Key]
        public int BoletaDetalleId { get; set; }
        public int BoletaCabeceraId { get; set; }
        public int ServicioId { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        [ForeignKey("BoletaCabeceraId")]
        public virtual BoletaCabecera BoletaCabecera { get; set; }
        public virtual Servicio Servicio { get; set; }
    }
}