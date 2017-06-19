using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioBackend.DataObjects
{
    [Table("Practicas_Detalle")]
    public class PracticasDetalle : EntityData
    {

        public string IdPractica { get; set; }
        public double Numero { get; set; }
        public double Minutos { get; set; }
        public bool Resultado { get; set; }

    }
}