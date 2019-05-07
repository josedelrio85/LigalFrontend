using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class InspeccionesGpsVM
    {
        public gen_inspecciones inspeccion { get; set; }
        public gen_usuarios usuario { get; set; }

        public List<gen_usuarios> listaUsuarios { get; set; }

        public buscadorInspeccionesGps buscador { get; set; }

        
        public InspeccionesGpsVM() {

            inspeccion = new gen_inspecciones();
            usuario = new gen_usuarios();

            listaUsuarios = new List<gen_usuarios>();
        }
    }
}