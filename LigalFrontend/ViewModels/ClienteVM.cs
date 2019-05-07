using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.ViewModels
{
    public class ClienteVM
    {
        public gen_clientes cliente { get; set; }
        public buscadorClientes buscador { get; set; }

        public ClienteVM()
        {
            cliente = new gen_clientes();
            buscador = new buscadorClientes();
        }
}
}