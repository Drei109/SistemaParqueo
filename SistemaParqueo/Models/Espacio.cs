namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Espacio")]
    public partial class Espacio
    {
        public int EspacioId { get; set; }

        public int? CocheraId { get; set; }

        public int? EspacioEstadoId { get; set; }

        public virtual Cochera Cochera { get; set; }

        public virtual EspacioEstado EspacioEstado { get; set; }
    }
}
