using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaParqueo.Models;

namespace SistemaParqueo.DTO
{
    public class CocheraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public string Foto { get; set; }
        public int? CocheraEstadoId { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string HorarioAtencion { get; set; }
        public List<Servicio> Servicios { get; set; }
    }
}