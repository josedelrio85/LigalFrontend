using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class ClientesEnPuntoVM
    {
        public Gen_clientesenpunto clientesEnPunto { get; set; }
        public gen_clientes cliente { get; set; }
        public GEN_PUNTOSRECOGIDA puntosRecogida { get; set; }

        public List<gen_clientes> listaClientes { get; set; }
        public List<GEN_PUNTOSRECOGIDA> listaPuntosRecogida { get; set; }

        public buscadorClientesenPunto buscador { get; set; }

        public ClientesEnPuntoVM()
        {
            buscador = new buscadorClientesenPunto();
            puntosRecogida = new GEN_PUNTOSRECOGIDA();
            cliente = new gen_clientes();
            clientesEnPunto = new Gen_clientesenpunto();
        }
    }
}