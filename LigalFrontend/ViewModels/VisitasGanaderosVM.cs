using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class VisitasGanaderosVM
    {
        public gen_visitavet visitasVet { get; set; }
        public gen_usuarios usuario { get; set; }

        public List<gen_usuarios> listaUsuarios { get; set; }

        public buscadorVisitasGanadero buscador { get; set; }

        public VisitasGanaderosVM()
        {
            visitasVet = new gen_visitavet();
            usuario = new gen_usuarios();

            listaUsuarios = new List<gen_usuarios>();
        }
    }
}