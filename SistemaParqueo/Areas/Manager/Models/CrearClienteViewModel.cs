using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaParqueo.Areas.Manager.Models
{
    public class CrearClienteViewModel
    {
        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(250)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(8)]
        public string DNI { get; set; }
        [Required]
        [StringLength(20)]
        public string Placa { get; set; }
    }
}