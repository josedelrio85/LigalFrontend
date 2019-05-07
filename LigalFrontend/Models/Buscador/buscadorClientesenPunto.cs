
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorClientesenPunto
    {
        [Display(Name="Código Punto")]
        public string CODIGOP { get; set; }
        [Display(Name="Cliente")]
        public string NOMBREC { get; set; }

        public buscadorClientesenPunto(){}
    }
}