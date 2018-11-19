using Newtonsoft.Json;

namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reserva")]
    public partial class Reserva
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reserva()
        {
            ReservaServicios = new HashSet<ReservaServicios>();
        }

        [JsonProperty(PropertyName = "id")]
        public int ReservaId { get; set; }

        public int VehiculoId { get; set; }

        public int ServicioId { get; set; }

        public TimeSpan? horaReservaLlegada { get; set; }

        public TimeSpan? horaReservaSalida { get; set; }

        public TimeSpan? horaSistemaLLegada { get; set; }

        public TimeSpan? horaSistemaSalida { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        public int? ReservaEstadoId { get; set; }

        public virtual ReservaEstado ReservaEstado { get; set; }

        public virtual Servicio Servicio { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservaServicios> ReservaServicios { get; set; }
    }
}
