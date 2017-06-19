using System;

namespace Mario.Models
{
    public class Practica 
    {

        public string id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double Minutos { get; set; }
        public double Resultado { get; set; }
        public string ResultColor
        {
            get
            {
                if (Resultado < 75)
                {
                    return "#FF5633";
                }
                else
                {
                    return "#A6FF33";
                }
            }
        }
    }
}