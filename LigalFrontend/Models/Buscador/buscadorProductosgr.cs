
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorProductosgr
    {
        [Display(Name="Código Producto")]
        public string CODIGOPRO { get; set; }
        [Display(Name="Producto")]
        public string PRODUCTO { get; set; }

        public buscadorProductosgr(){}
    }
}