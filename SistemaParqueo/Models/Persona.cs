namespace SistemaParqueo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Persona")]
    public partial class Persona
    {
        public int PersonaId { get; set; }

        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(250)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(8)]
        public string DNI { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\/]?){0,})(?:[\-\.\ \\/]?(?:#|ext\.?|extension|x)[\-\.\ \\/]?(\d+))*$", ErrorMessage = "Número de teléfono no válido")]
        public string Telefono { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\/]?){0,})(?:[\-\.\ \\/]?(?:#|ext\.?|extension|x)[\-\.\ \\/]?(\d+))*$", ErrorMessage = "Número de celular no válido")]
        public string Celular { get; set; }

        [StringLength(250, MinimumLength = 10)]
        public string Direccion { get; set; }

        //[StringLength(250)]
        //public string Email { get; set; }

        public int? PersonaEstadoId { get; set; }

        [StringLength(128)]
        public string AspNetUsersId { get; set; }

        public virtual PersonaEstado PersonaEstado { get; set; }

        [ForeignKey("AspNetUsersId")]
        public virtual ApplicationUser User { get; set; }


    }
}
