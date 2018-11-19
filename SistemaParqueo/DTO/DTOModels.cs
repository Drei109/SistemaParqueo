using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaParqueo.Models;
// ReSharper disable InconsistentNaming

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

    public class ReservaDTO
    {
        public decimal costo { get; set; }
        public TimeSpan? hora_reserva_llegada { get; set; }

        public TimeSpan? hora_reserva_salida { get; set; }

        public TimeSpan? hora_sistema_llegada { get; set; }

        public TimeSpan? hora_sistema_salida { get; set; }
        public int id { get; set; }
        public int? reserva_estado_id { get; set; }
        public int servicio_id { get; set; }
        public int vehiculo_id { get; set; }

    }

}