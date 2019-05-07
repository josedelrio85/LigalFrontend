using LigalFrontend.Models;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class UsuariosVehiculoVM
    {
        public GEN_USUARIOSVEHICULO usuVehiculo { get; set; }
        public gen_usuarios usuario { get; set; }
        public GEN_VEHICULO vehiculo { get; set; }

        public List<gen_usuarios> listaUsuarios { get; set; }


        public UsuariosVehiculoVM()
        {
            usuVehiculo = new GEN_USUARIOSVEHICULO();
            usuario = new gen_usuarios();
        }
    }
}