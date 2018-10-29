namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CocheraUsuario")]
    public partial class CocheraUsuario
    {
        public int CocheraUsuarioId { get; set; }

        public int CocheraId { get; set; }

        public virtual Cochera Cochera { get; set; }

        [StringLength(128)]
        public string AspNetUsersId { get; set; }

        [ForeignKey("AspNetUsersId")]
        public virtual ApplicationUser User { get; set; }
    }
}
