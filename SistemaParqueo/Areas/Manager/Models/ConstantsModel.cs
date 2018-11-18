﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaParqueo.Areas.Manager.Models
{
    public class EstadoReserva
    {
        public static readonly int Enviado = 1;
        public static readonly int Cancelado = 2;
        public static readonly int Terminado = 3;
        public static readonly int Generado = 4;
    }
}