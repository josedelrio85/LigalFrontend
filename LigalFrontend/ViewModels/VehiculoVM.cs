using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class VehiculoVM
    {
        public GEN_VEHICULO vehiculo { get; set; }

        public List<UsuariosVehiculoVM> listaUsuariosVehiculo { get; set; }

        public buscadorVehiculo buscador { get; set; }

        public VehiculoVM() { }
    }
}