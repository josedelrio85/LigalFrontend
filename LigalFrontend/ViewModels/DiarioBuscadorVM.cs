using LigalFrontend.Models.Buscador;
using PagedList;

namespace LigalFrontend.ViewModels
{
    public class DiarioBuscadorVM
    {
        public IPagedList<DiarioMatriculaVM> diariovm { get; set; }
        public buscadorDiario buscador { get; set; }

        public DiarioBuscadorVM()
        {
            buscador = new buscadorDiario();
        }
    }
}