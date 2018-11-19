namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servicio")]
    public partial class Servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servicio()
        {
            Reserva = new HashSet<Reserva>();
            ReservaServicios = new HashSet<ReservaServicios>();
        }

        public int ServicioId { get; set; }

        public int CocheraId { get; set; }

        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; }

        [Column(TypeName = "money")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal? Costo { get; set; }

        public bool? EsPorHora { get; set; }

        public virtual Cochera Cochera { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservaServicios> ReservaServicios { get; set; }
    }
}
