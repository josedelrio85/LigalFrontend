
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorClientes
    {
        [Display(Name="Código")]
        public string CODIGOC { get; set; }
        [Display(Name="Nombre")]
        public string NOMBREC { get; set; }
        [Display(Name="Población")]
        public string POBLACION { get; set; }

        public buscadorClientes(){}
    }
}