using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class SeguimientoGpsVM
    {
        public GEN_COORDENADASGPS coordenadasGps { get; set; }
        public gen_usuarios usuario { get; set; }

        public List<gen_usuarios> listaUsuarios { get; set; }

        public buscadorSeguimientoGps buscador { get; set; }

        public SeguimientoGpsVM()
        {
            coordenadasGps = new GEN_COORDENADASGPS();
            usuario = new gen_usuarios();

            listaUsuarios = new List<gen_usuarios>();
        }
    }
}