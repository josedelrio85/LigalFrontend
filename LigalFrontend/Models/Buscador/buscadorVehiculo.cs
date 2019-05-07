
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorVehiculo
    {
        [Display(Name = "Matrícula")]
        public string MATRICULA { get; set; }
        [Display(Name = "Modelo")]
        public string MODELO { get; set; }
        [Display(Name = "Año Compra")]
        public string ANHOCOMPRA { get; set; }
        [Display(Name = "Combustible")]
        public string COMBUSTIBLE { get; set; }

        public buscadorVehiculo(){}
    }
}