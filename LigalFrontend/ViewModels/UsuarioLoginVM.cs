using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.ViewModels
{
    public class UsuarioLoginVM
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Introduzca un Login")]
        [RegularExpression("(^[a-zA-Z0-9_-]+$)", ErrorMessage = "Introduza únicamente caracteres alfanúmericos, _ o -")]
        public string LOGIN { get; set; }
        [Display(Name = "Password")]
        [RegularExpression("(^[a-zA-Z0-9_-]+$)", ErrorMessage = "Introduza únicamente caracteres alfanúmericos, _ o -")]
        public string PASSWORD { get; set; }

    }
}