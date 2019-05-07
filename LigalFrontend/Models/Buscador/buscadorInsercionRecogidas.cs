using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorInsercionRecogidas
    {
        [Display(Name="Código Punto")]
        public string codpunto { get; set; }
        [Display(Name="Programado")]
        public string programado { get; set; }
        //public string visitado { get; set; }
        [Display(Name="Fecha Inicio")]
        public string fechaInicio { get; set; }
        [Display(Name="Fecha Fin")]
        public string fechaFin { get; set; }
        [Display(Name="Usuario")]
        public string idUsuario { get; set; }
        [Display(Name = "Nombre Punto")]
        public string nompunto { get; set; }

        public List<gen_usuarios> listaUsuarios { get; set; }


        public buscadorInsercionRecogidas() {
            listaUsuarios = new List<gen_usuarios>();
        }
    }
}