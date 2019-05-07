using LigalFrontend.Models.Buscador;
using PagedList;

namespace LigalFrontend.ViewModels
{
    public class InspBuscadorVM
    {
        public IPagedList<InspeccionesVM> inspecvm { get; set; }
        public buscadorInspecciones buscador { get; set; }

        public InspBuscadorVM()
        {
            buscador = new buscadorInspecciones();
        }
    }
}