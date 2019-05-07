

using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorSeguimientoGps
    {
        [Display(Name = "Fecha Visita Inicio")]
        public string FechaHoraVisitaI { get; set; }
        [Display(Name = "Fecha Visita Fin")]
        public string FechaHoraVisitaF { get; set; }
        [Display(Name = "Usuario")]
        public string idUsuario { get; set; }

        public buscadorSeguimientoGps() { }
    }
}