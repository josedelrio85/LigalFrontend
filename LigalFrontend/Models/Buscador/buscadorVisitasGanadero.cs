﻿
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorVisitasGanadero
    {
        [Display(Name = "Fecha Visita Inicio")]
        public string FechaHoraVisitaI { get; set; }
        [Display(Name = "Fecha Visita Fin")]
        public string FechaHoraVisitaF { get; set; }
        [Display(Name = "Serie Ganadero")]
        public string serieGanadero { get; set; }
        [Display(Name = "Usuario")]
        public string idUsuario { get; set; }


        [Display(Name = "Código Muestra")]
        public int codMuestra { get; set; }

        [Display(Name = "Código Muestra")]
        public bool BoolCodMuestra { get; set; }

        public buscadorVisitasGanadero(){}
    }
}