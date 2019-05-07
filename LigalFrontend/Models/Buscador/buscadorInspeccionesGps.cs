using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorInspeccionesGps
    {
        [Display(Name = "Fecha Visita Inicio")]
        public string FechaHoraVisitaI { get; set; }
        [Display(Name = "Fecha Visita Fin")]
        public string FechaHoraVisitaF { get; set; }
        [Display(Name = "Tipo Inspección")]
        public string TipoInspeccion { get; set; }
        [Display(Name = "Usuario")]
        public string idUsuario { get; set; }

        [Display(Name = "Industria")]
        public string Industria { get; set; }

        public buscadorInspeccionesGps() { }
    }
}