using Microsoft.Azure.Mobile.Server;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioBackend.DataObjects
{
    [Table("Practicas")]
    public class Practica : EntityData 
    {

        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double Minutos { get; set; }
        public double Resultado { get; set; }

    }
}