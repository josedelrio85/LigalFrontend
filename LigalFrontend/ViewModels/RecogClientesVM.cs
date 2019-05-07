using LigalFrontend.Models;

using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class RecogClientesVM
    {
        public gen_recogidasR recogidasR { get; set; }
        public gen_clientes cliente { get; set; }
        public gen_observa observa { get; set; }

        public List<gen_clientes> listaClientes { get; set; }
        public List<gen_observa> listaObservaciones { get; set; }

        public List<ProductosgrVM> listaProductosgr { get; set; }

        public ImageSliderVM listaImagenes { get; set; }

        public RecogClientesVM() { }
    }
}