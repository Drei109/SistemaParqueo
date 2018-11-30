using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaParqueo.Models
{
    public class BoletaCabecera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoletaCabecera()
        {
            BoletaDetalle = new HashSet<BoletaDetalle>();
        }

        [Key]
        public int BoletaCabeceraId { get; set; }
        public int ReservaId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public string Estado { get; set; }
        [Column(TypeName = "money")]
        public decimal Subtotal { get; set; }
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoletaDetalle> BoletaDetalle { get; set; }
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente{ get; set; }
        [ForeignKey("ReservaId")]
        public virtual Reserva Reserva{ get; set; }
    }
}