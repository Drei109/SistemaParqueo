namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cochera")]
    public partial class Cochera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cochera()
        {
            CocheraUsuario = new HashSet<CocheraUsuario>();
            Espacio = new HashSet<Espacio>();
            Servicio = new HashSet<Servicio>();
        }

        public int CocheraId { get; set; }

        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Direccion { get; set; }

        [StringLength(250)]
        [Required]
        public string Descripcion { get; set; }

        [StringLength(250)]
        public string Longitud { get; set; }

        [StringLength(250)]
        public string Latitud { get; set; }

        [StringLength(250)]
        public string Foto { get; set; }

        [StringLength(30)]
        public string Telefono { get; set; }

        [StringLength(250)]
        public string HorarioAtencion { get; set; }

        public int? EmpresaId { get; set; }

        public int? CocheraEstadoId { get; set; }

        [StringLength(9)]
        public string CodigoPostal { get; set; }

        public virtual CocheraEstado CocheraEstado { get; set; }

        public virtual Empresa Empresa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CocheraUsuario> CocheraUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Espacio> Espacio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
