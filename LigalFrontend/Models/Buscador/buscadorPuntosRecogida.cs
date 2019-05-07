
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorPuntosRecogida
    {
        [Display(Name = "Código Punto")]
        public string CODIGOP { get; set; }
        [Display(Name = "Nombre Punto")]
        public string NOMBRE { get; set; }
        [Display(Name = "Recogedor")]
        public string RECOGEDOR { get; set; }

        public buscadorPuntosRecogida(){}
    }
}