using Xamarin.Forms;

namespace Mario.Models
{
    public class PracticasDetalle 
    {

        public string id { get; set; }
        public string IdPractica { get; set; }
        public double Numero { get; set; }
        public double Minutos { get; set; }
        public bool Resultado { get; set; }
        public ImageSource ImagenResultado
        {
            get
            {
                if (Resultado)
                {
                    return ImageSource.FromResource("Mario.Images.1497672196_ok.png");
                }
                else
                {
                    return ImageSource.FromResource("Mario.Images.1497672242_cancel.png");
                }
            }
        }
        public string FormattedTime
        {
            get
            {                
                int minutes = (int)(Minutos / 60);
                int seconds = (int)(Minutos - (minutes * 60));
                return string.Format("{0}:{1}", minutes.ToString("D2"), seconds.ToString("D2"));
            }
        }
    }
}