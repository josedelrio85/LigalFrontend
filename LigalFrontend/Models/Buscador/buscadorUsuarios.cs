
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorUsuarios
    {
        [Display(Name = "Login")]
        public string LOGIN { get; set; }
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }
        [Display(Name = "Tipo Usuario")]
        public string TIPOUSUARIO { get; set; }

        public buscadorUsuarios(){}
    }
}