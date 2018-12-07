using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SistemaParqueo.Models;

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

    public class AddServiciosToReservaViewModel
    {
        public List<Servicio> Servicios { get; set; }
        public int ReservaId { get; set; }
        public int ServicioId { get; set; }
        [Column(TypeName = "money")]
        public decimal Costo { get; set; }
        public int? Cantidad { get; set; }
    }
}