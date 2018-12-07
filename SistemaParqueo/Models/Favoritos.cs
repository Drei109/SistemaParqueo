using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaParqueo.Models
{
    [Table("Favoritos")]
    public class Favoritos
    {
        [Key, Column(Order = 0)]
        public int ClienteId { get; set; }
        [Key, Column(Order = 1)]
        public int CocheraId { get; set; }

        public virtual Cochera Cochera { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}