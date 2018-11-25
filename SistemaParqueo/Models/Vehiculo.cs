namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehiculo")]
    public partial class Vehiculo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehiculo()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int VehiculoId { get; set; }

        public int? ClienteId { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string Placa { get; set; }

        public int? TipoVehiculoId { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }

        public virtual TipoVehiculo TipoVehiculo { get; set; }
    }
}
