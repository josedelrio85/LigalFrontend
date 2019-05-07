using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class PuntosRecogidaVM
    {
        public GEN_PUNTOSRECOGIDA puntosRecogida { get; set; }
        public gen_usuarios usuario { get; set; }

        public List<gen_usuarios> listaUsuarios { get; set; }

        public buscadorPuntosRecogida buscador { get; set; }


        public PuntosRecogidaVM()
        {
            puntosRecogida = new GEN_PUNTOSRECOGIDA();
            usuario = new gen_usuarios();

            listaUsuarios = new List<gen_usuarios>();
        }
    }
}